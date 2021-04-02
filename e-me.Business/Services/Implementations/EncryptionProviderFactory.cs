using e_me.Business.Services.Interfaces;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;

namespace e_me.Business.Services.Implementations
{
    public class EncryptionProviderFactory : IEncryptionProviderFactory
    {
        public IEncryptionProvider Create()
        {
            return CreateAesProvider();
        }

        private AesProvider CreateAesProvider()
        {
            var keyInfo = AesProvider.GenerateKey(AesKeySize.AES256Bits);
            return new AesProvider(keyInfo.Key);
        }
    }
}
