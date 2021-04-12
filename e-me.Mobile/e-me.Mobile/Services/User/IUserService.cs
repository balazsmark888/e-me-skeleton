using System.Threading.Tasks;
using e_me.Shared.DTOs;
using e_me.Shared.DTOs.User;

namespace e_me.Mobile.Services.User
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(UserRegistrationDto registrationDto);

        Task<UserDto> LoginAsync(AuthDto authDto);

        Task<bool> LogoutAsync();
    }
}
