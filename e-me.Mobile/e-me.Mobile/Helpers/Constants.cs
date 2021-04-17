namespace e_me.Mobile.Helpers
{
    public static class Constants
    {
        public const string AuthTokenProperty = "AuthToken";
        public const string ServerPublicKeyProperty = "ServerPublicKey";
        public const string E2EEIVProperty = "E2EEIV";

        public const string BackendBaseAddressProperty = "BackendBaseAddress";
        public const string GreetingMessageProperty = "GreetingMessage";

        #region Addresses

        public const string RegisterAddress = "/api/auth/register";
        public const string LoginAddress = "/api/auth/login";
        public const string LogoutAddress = "/api/auth/logout";

        #endregion
    }
}
