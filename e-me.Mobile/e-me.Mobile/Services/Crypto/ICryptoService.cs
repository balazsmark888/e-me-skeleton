using e_me.Mobile.Models;
using e_me.Shared.DTOs.User;

namespace e_me.Mobile.Services.Crypto
{
    public interface ICryptoService
    {
        void SaveKeyInformation(UserDto userDto);

        byte[] CreatePublicKey();

        EcdhKeyInformation GetKeyInformation();
    }
}
