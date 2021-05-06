using System;
using System.Net.Http;
using System.Threading.Tasks;
using e_me.Shared.DTOs;
using e_me.Shared.DTOs.User;

namespace e_me.Mobile.Services.User
{
    public interface IUserService
    {
        Task<HttpResponseMessage> RegisterAsync(UserRegistrationDto registrationDto);

        Task<UserDto> LoginAsync(AuthDto authDto);

        Task<HttpResponseMessage> LogoutAsync();

        Task<UserDetailDto> GetUserDetailAsync(Guid userId);

        Task<bool> UpdateUserDetailAsync(UserDetailDto userDetailDto);

        bool IsAuthenticated();
    }
}
