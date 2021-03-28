using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using e_me.Business.DTOs;
using e_me.Model.Models;

namespace e_me.Mvc.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> AuthenticateAsync(string userName, string password);
        Task DeAuthenticateAsync();
        Task<bool> IsValidCurrentTokenAsync();
        Task<User> GetAuthenticatedUserAsync();
        Task<bool> IsValidCurrentResetPasswordTokenAsync(string resetPasswordToken);
        Task<bool> ResetUserPassword(string token, string password);
        Task<(string FullName, string Email, string Token)> GenerateResetPasswordAsync(string email);
    }
}
