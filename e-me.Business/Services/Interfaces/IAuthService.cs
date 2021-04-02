using System.Threading.Tasks;
using e_me.Business.DTOs;
using e_me.Model.Models;

namespace e_me.Business.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> AuthenticateAsync(AuthDto authDto);
        Task DeAuthenticateAsync();
        Task<bool> IsValidCurrentTokenAsync();
        Task<User> GetAuthenticatedUserAsync();
    }
}
