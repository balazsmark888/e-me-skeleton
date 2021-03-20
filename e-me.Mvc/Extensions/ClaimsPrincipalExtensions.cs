using System;
using System.Linq;
using System.Security.Claims;
using e_me.Model.Repositories;
using Microsoft.AspNetCore.Http;

namespace e_me.Mvc.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid UserId(this ClaimsPrincipal claimsPrincipal) =>
            Guid.Parse(claimsPrincipal.Claims.First(c => c.Type == "UserId").Value);

        public static Guid GetUserIdBasedOnToken(this HttpContext httpContext, IJwtTokenRepository jwtTokenRepository)
        {
            httpContext.Request.Headers.TryGetValue("Authorization", out var token);
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException("Authorization header not found!");
            }

            var tokenValue = token.ToString().Substring(7);
            var jwtEntry = jwtTokenRepository.All.FirstOrDefault(s => s.Token.Equals(tokenValue));

            if (jwtEntry == null)
            {
                throw new System.AccessViolationException("Token unauthorized");
            }

            return jwtEntry.UserId;
        }
    }
}
