namespace e_me.Mobile.Helpers
{
    public static class Constants
    {
        public const string AuthTokenProperty = "AuthToken";
        public const string ServerPublicKeyProperty = "ServerPublicKey";
        public const string ClientPrivateKeyProperty = "ClientPrivateKey";
        public const string ClientPublicKeyProperty = "ClientPublicKey";
        public const string CurrentDocumentProperty = "CurrentDocument";
        public const string ShareDocumentProperty = "ShareDocument";

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
        public const string DocumentTemplateOwnedListItemGetAddress = "/api/documenttemplates/owned";

        public const string UserDocumentListItemGetAddress = "/api/userdocuments/list";
        public const string UserDocumentGetAddress = "/api/userdocuments/document";
        public const string UserDocumentDeleteAddress = "/api/userdocuments/delete";
        public const string UserDocumentRequestFromTemplateAddress = "/api/userdocuments/requestfromtemplate";
        public const string AccessTokenGetAddress = "/api/onetimeaccesstokens/requesttoken";
        public const string UserDocumentFromCodeGetAddress = "/api/userdocuments/requestfromcode";

        public const string UserDetailGetAddress = "/api/userdetails/getbyuser";
        public const string UserDetailUpdateAddress = "/api/userdetails/update";
        #endregion
    }
}
