using System;
using e_me.Mobile.AppContext;
using e_me.Mobile.Helpers;
using e_me.Mobile.Models;

namespace e_me.Mobile.Services.Crypto
{
    public class ApplicationCryptoService : ICryptoService
    {
        private readonly ApplicationContext _applicationContext;

        public ApplicationCryptoService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public EcdhKeyInformation GetKeyInformation()
        {
            return _applicationContext.ApplicationSecureStorage[Constants.EcdhKeyInformationModelProperty] as EcdhKeyInformation;
        }

        public byte[] CreatePublicKey()
        {
            throw new NotImplementedException();
        }
    }
}
