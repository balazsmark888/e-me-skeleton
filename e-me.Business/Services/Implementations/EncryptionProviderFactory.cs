using System.Security.Cryptography;
using System.Text;
using e_me.Business.Services.Interfaces;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.Extensions.Options;

namespace e_me.Business.Services.Implementations
{
    public class EncryptionProviderFactory : IEncryptionProviderFactory
    {
        private readonly AuthSettings _authSettings;

        public EncryptionProviderFactory(IOptions<AuthSettings> authSettings)
        {
            _authSettings = authSettings.Value;
        }

        public IEncryptionProvider Create()
        {
            return CreateAesProvider();
        }

        private AesProvider CreateAesProvider()
        {
            var key = Encoding.ASCII.GetBytes(_authSettings.SecretKey);
            var provider = new AesProvider(key, CipherMode.CBC, PaddingMode.Zeros);
            return provider;
        }
    }
}
