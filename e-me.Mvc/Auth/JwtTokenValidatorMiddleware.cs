using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using e_me.Mvc.Auth.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace e_me.Mvc.Auth
{
    public class JwtTokenValidatorMiddleware
    {
        private readonly JwtTokenValidatorOptions options;
        private readonly RequestDelegate next;

        public JwtTokenValidatorMiddleware(JwtTokenValidatorOptions options, RequestDelegate next)
        {
            this.options = options;
            this.next = next;
        }

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
                await next(context);
                return;
            }

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }

        private async Task<bool> Validate(HttpContext context, IAuthService authService)
        {
            if (options.IsAdditionalUrl(context.Request.Path))
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

    public static class ApplicationBuilderExtension
    {
        public static void UseJwtTokenValidator(this IApplicationBuilder builder, JwtTokenValidatorOptions options)
        {
            builder.UseMiddleware<JwtTokenValidatorMiddleware>(options);
        }
    }
}
