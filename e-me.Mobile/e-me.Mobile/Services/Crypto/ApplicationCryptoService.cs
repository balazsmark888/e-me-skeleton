using e_me.Mobile.AppContext;
using e_me.Mobile.Helpers;
using e_me.Mobile.Models;
using e_me.Shared;
using e_me.Shared.Communication;
using e_me.Shared.DTOs.User;

namespace e_me.Mobile.Services.Crypto
{
    public class ApplicationCryptoService : ICryptoService
    {
        private readonly ApplicationContext _applicationContext;

        public ApplicationCryptoService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void SaveKeyInformation(UserDto userDto)
        {
            var clientPublicKey = _applicationContext.ApplicationSecureStorage[Constants.ClientPublicKeyProperty] as byte[];
            var clientPrivateKey = _applicationContext.ApplicationSecureStorage[Constants.ClientPrivateKeyProperty] as byte[];

            var keyStore = new EcdhKeyStore(clientPrivateKey, clientPublicKey);
            keyStore.SetPeerPublicKey(userDto.PublicKey);

            var model = new EcdhKeyInformation
            {
                ClientPublicKey = clientPublicKey.ToBase64String(),
                DerivedHmacKey = keyStore.DerivedHmacKey.ToBase64String(),
                HmacKey = keyStore.HmacKey.ToBase64String(),
                IV = userDto.IV.ToBase64String(),
                ServerPublicKey = userDto.PublicKey.ToBase64String(),
                SharedKey = keyStore.SharedKey.ToBase64String()
            };
            _applicationContext.ApplicationSecureStorage[Constants.EcdhKeyInformationModelProperty] = model;
        }

        public EcdhKeyInformation GetKeyInformation()
        {
            return _applicationContext.ApplicationSecureStorage[Constants.EcdhKeyInformationModelProperty] as EcdhKeyInformation;
        }

        public byte[] CreatePublicKey()
        {
            var keyStore = new EcdhKeyStore();
            _applicationContext.ApplicationSecureStorage[Constants.ClientPrivateKeyProperty] = keyStore.PrivateKey;
            _applicationContext.ApplicationSecureStorage[Constants.ClientPublicKeyProperty] = keyStore.PublicKey;
            return keyStore.PublicKey;
        }
    }
}
