namespace e_me.Mobile.Helpers
{
    public static class Constants
    {
        public const string AuthTokenProperty = "AuthToken";
        public const string ServerPublicKeyProperty = "ServerPublicKey";
        public const string ClientPrivateKeyProperty = "ClientPrivateKey";
        public const string ClientPublicKeyProperty = "ClientPublicKey";
        public const string E2EEIVProperty = "E2EEIV";

        public const string DocumentTypesProperty = "DocumentTypes";

        public const string EcdhKeyInformationModelProperty = "EcdhKeyInformation";

        public const string BackendBaseAddressProperty = "BackendBaseAddress";
        public const string GreetingMessageProperty = "GreetingMessage";

        #region Addresses

        public const string RegisterAddress = "/api/auth/register";
        public const string LoginAddress = "/api/auth/login";
        public const string LogoutAddress = "/api/auth/logout";
        public const string ValidateAddress = "/api/auth/validate";

        public const string DocumentTypeGetAddress = "/api/documenttypes/getall";
        public const string DocumentTemplateListItemGetAddress = "/api/documenttemplates/list";
        public const string DocumentTemplateAvailableListItemGetAddress = "/api/documenttemplates/available";

        public const string UserDocumentListItemGetAddress = "/api/userdocuments/list";
        public const string UserDocumentGetAddress = "/api/userdocuments/";
        public const string UserDocumentRequestFromTemplateAddress = "/api/userdocuments/requestfromtemplate";

        #endregion
    }
}
