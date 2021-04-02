using System;
using System.Linq;
using System.Threading.Tasks;
using e_me.Business.DTOs;
using e_me.Model.Models;

namespace e_me.Business.Services.Interfaces
{
    public interface IUserService
    {
        IQueryable<User> All { get; }

        Task<bool> UpdateAsync(Guid id);

        Task<User> CreateAsync();

        Task<bool> ChangePasswordLoggedUserAsync(User user);

        Task<bool> ChangePasswordAsync(User user);

        Task<bool> DeleteAsync(Guid id);

        Task<User> UpdateAsync(User user);

        Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);

        Task<bool> UpdateUserAsync(UserProfileDto userProfileDto);

        Task<UserProfileDto> GetUserDtoAsync(Guid userId);

        Task<bool> ResetUserPassword(User user, string newPassword);

        Task<UserProfileDto> CreateUserAsync(UserRegistrationDto userRegistrationDto);

        Task<string> ChangePasswordAsync(Guid id);
    }
}
