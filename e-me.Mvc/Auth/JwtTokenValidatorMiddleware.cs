using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using e_me.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace e_me.Mvc.Auth
{
    /// <summary>
    /// Middleware for validating JWT tokens.
    /// </summary>
    public class JwtTokenValidatorMiddleware
    {
        private readonly JwtTokenValidatorOptions _options;
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor that initializes the options.
        /// </summary>
        /// <param name="options">Options for the validator.</param>
        /// <param name="next">Next delegate in the pipeline.</param>
        public JwtTokenValidatorMiddleware(JwtTokenValidatorOptions options, RequestDelegate next)
        {
            _options = options;
            _next = next;
        }

        /// <summary>
        /// Validates the current Http context and stores the identity of the user.
        /// </summary>
        /// <param name="context">The Http context.</param>
        /// <param name="authService">The authentication service.</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, IAuthService authService)
        {
            if (await Validate(context, authService))
            {
                if (IsAuthenticated(context))
                {
                    var user = await authService.GetAuthenticatedUserAsync();
                    var claims = new List<Claim>
                    {
                        new("UserId", user.Id.ToString())
                    };
                    
                    var appIdentity = new ClaimsIdentity(claims);
                    context.User.AddIdentity(appIdentity);
                }
                await _next(context);
                return;
            }

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }

        private async Task<bool> Validate(HttpContext context, IAuthService authService)
        {
            if (_options.IsAdditionalUrl(context.Request.Path))
            {
                return IsAuthenticated(context) && await authService.IsValidCurrentTokenAsync();
            }

            return !IsAuthenticated(context) || await authService.IsValidCurrentTokenAsync();
        }

        private bool IsAuthenticated(HttpContext httpContext)
        {
            var principal = httpContext.User;
            return principal != null && principal.Identity.IsAuthenticated;
        }
    }
    /// <summary>
    /// Options class for the JWT token validator.
    /// </summary>
    public class JwtTokenValidatorOptions
    {
        public JwtTokenValidatorOptions()
        {
            AdditionalUrls = new List<string>();
        }

        public List<string> AdditionalUrls { get; set; }

        public bool IsAdditionalUrl(string url)
        {
            return url.Length > 1 && AdditionalUrls.Any(u => url.ToLower().StartsWith(u.ToLower()));
        }
    }
}
