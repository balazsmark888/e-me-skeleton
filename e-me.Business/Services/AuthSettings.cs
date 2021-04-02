using System;

namespace e_me.Business.Services
{
    /// <summary>
    /// Class for authentication-related settings.
    /// </summary>
    public class AuthSettings
    {
        /// <summary>
        /// Issuer for the JWT tokens.
        /// </summary>
        public string Issuer => "E-me.Web";

        /// <summary>
        /// Secret key for token generation.
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// The timespan until the generated tokens are valid for.
        /// </summary>
        public TimeSpan TokenLifeTimeDuration { get; set; }
    }
}
