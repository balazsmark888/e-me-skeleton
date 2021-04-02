using Microsoft.EntityFrameworkCore.DataEncryption;

namespace e_me.Business.Services.Interfaces
{
    public interface IEncryptionProviderFactory
    {
        IEncryptionProvider Create();
    }
}
