using e_me.Mobile.AppContext;
using e_me.Mobile.Helpers;
using e_me.Mobile.Models;
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
            keyStore.SetOtherPartyPublicKey(userDto.PublicKey);

            var model = new EcdhKeyInformation
            {
                ClientPublicKey = clientPublicKey,
                DerivedHmacKey = keyStore.DerivedHmacKey,
                HmacKey = keyStore.HmacKey,
                IV = userDto.IV,
                ServerPublicKey = userDto.PublicKey,
                SharedKey = keyStore.SharedKey
            };
            _applicationContext.ApplicationSecureStorage[Constants.EcdhKeyInformationModelProperty] = model;
        }

        public byte[] CreatePublicKey()
        {
            var keyStore = new EcdhKeyStore();
            _applicationContext.ApplicationSecureStorage[Constants.ClientPrivateKeyProperty] = keyStore.PrivateKey;
            _applicationContext.ApplicationSecureStorage[Constants.ClientPublicKeyProperty] = keyStore.PublicKey;
            return keyStore.PublicKey;
        }
    }

    public interface ICryptoService
    {
        void SaveKeyInformation(UserDto userDto);

        byte[] CreatePublicKey();
    }
}
