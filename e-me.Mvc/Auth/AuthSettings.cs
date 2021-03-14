using System;

namespace e_me.Mvc.Auth
{
    public class AuthSettings
    {
        public string Issuer => "E-me.Web";

        public string SecretKey { get; set; }

        public TimeSpan TokenLifeTimeDuration { get; set; }
    }
}
