using System;
using System.Threading.Tasks;
using e_me.Model.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace e_me.Mvc.Controllers.ValidationAttributes
{
    /// <summary>
    /// ActionFilterAttribute to validate UserIds.
    /// </summary>
    public class ValidateUserIdAttribute : ActionFilterAttribute
    {
        private readonly string _userIdName;


        public ValidateUserIdAttribute(string userIdName = "userId")
        {
            _userIdName = userIdName;
        }

        /// <summary>
        /// Verifies if the User of the specified UserId exists.
        /// </summary>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = GetUserId(context);
            if (userId == null)
            {
                await base.OnActionExecutionAsync(context, next);
                return;
            }

            var controller = GetController(context);
            var userRepository = controller.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
            var user = await userRepository.GetByIdAsync(userId.Value);
            if (user == null)
            {
                context.Result = new NotFoundObjectResult(userId);
                return;
            }

            await base.OnActionExecutionAsync(context, next);
        }

        private Guid? GetUserId(ActionExecutingContext context)
        {
            Guid? userId = null;
            if (context.ActionArguments.TryGetValue(_userIdName, out var userIdValue))
            {
                userId = userIdValue as Guid?;
            }

            return userId;
        }

        private static ControllerBase GetController(ActionExecutingContext context)
        {
            if (!(context.Controller is ControllerBase controller))
            {
                throw new ArgumentNullException(nameof(context.Controller));
            }

            return controller;
        }
    }
}
