using System;
using Microsoft.AspNetCore.Http;

namespace e_me.Core.Application
{
    public class ApplicationUserContextFactory : IApplicationUserContextFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationUserContextFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ApplicationUserContext Create()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var sessionId = Guid.NewGuid().ToString();
            return CreateApplicationUserContext(sessionId, httpContext);
        }



        private string GetCurrentUserName(HttpContext httpContext)
        {
            if (!IsAuthenticated(httpContext))
            {
                return string.Empty;
            }

            var principal = httpContext.User;
            return principal == null ? string.Empty : principal.Identity.Name;
        }

        private bool IsAuthenticated(HttpContext httpContext)
        {
            var principal = httpContext.User;
            return principal != null && principal.Identity.IsAuthenticated;
        }
        private ApplicationUserContext CreateApplicationUserContext(string sessionId, HttpContext httpContext)
        {
            return new ApplicationUserContext
            {
                ApplicationKey = ApplicationConfiguration.Instance.ApplicationKey,
                ConnectionString = ApplicationConfiguration.Instance.ConnectionString,
                CurrentSessionId = sessionId,
                CurrentUserName = GetCurrentUserName(httpContext)
            };
        }
    }
}
