using e_me.Mobile.Models;

namespace e_me.Mobile.Services.Crypto
{
    public interface ICryptoService
    {
        byte[] CreatePublicKey();

        EcdhKeyInformation GetKeyInformation();
    }
}
