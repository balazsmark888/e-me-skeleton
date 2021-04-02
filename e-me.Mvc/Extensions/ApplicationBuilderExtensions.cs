using e_me.Mvc.Auth;
using Microsoft.AspNetCore.Builder;

namespace e_me.Mvc.Extensions
{
    /// <summary>
    /// Extension methods for the ApplicationBuilder.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds the JwtTokenValidatorMiddleware middleware to the ApplicationBuilder.
        /// </summary>
        /// <param name="builder">The ApplicationBuilder.</param>
        /// <param name="options">Options object for the JWT validator.</param>
        public static void UseJwtTokenValidator(this IApplicationBuilder builder, JwtTokenValidatorOptions options)
        {
            builder.UseMiddleware<JwtTokenValidatorMiddleware>(options);
        }
    }
}
