using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using e_me.Business.DTOs;
using e_me.Business.Interfaces;
using e_me.Model.Models;
using e_me.Model.Repositories;
using e_me.Mvc.Auth.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace e_me.Mvc.Auth
{
    public class AuthService : IAuthService
    {
        public readonly AuthSettings AuthSettings;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IUserSecurityRoleRepository _userSecurityRoleRepository;
        private readonly IJwtTokenRepository _jwtTokenRepository;
        private readonly IResetPasswordTokenRepository _resetPasswordTokenRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository,
                     IUserService userService,
                     IUserSecurityRoleRepository userSecurityRoleRepository,
                     IJwtTokenRepository jwtTokenRepository,
                     IResetPasswordTokenRepository resetPasswordTokenRepository,
                     ITokenGenerator tokenGenerator,
                     IHttpContextAccessor httpContextAccessor,
                     IOptions<AuthSettings> authSettings,
                     ILogger<AuthService> logger)
        {
            AuthSettings = authSettings.Value;
            _userRepository = userRepository;
            _userService = userService;
            _userSecurityRoleRepository = userSecurityRoleRepository;
            _jwtTokenRepository = jwtTokenRepository;
            _resetPasswordTokenRepository = resetPasswordTokenRepository;
            _tokenGenerator = tokenGenerator;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<UserDto> AuthenticateAsync(string userName, string password)
        {
            var user = _userRepository.GetByUsernameAndPassword(userName, password);
            if (user == null)
            {
                return null;
            }

            var role = await _userRepository.GetUserRoleAsync(user.Id);
            var validTo = DateTime.UtcNow + AuthSettings.TokenLifeTimeDuration;
            var token = _tokenGenerator.Generate(userName, validTo, role);
            await SaveTokenAsync(user.Id, token, validTo);

            _logger.LogInformation("Successfully authenticated user:{User} as {Role}", userName, role);

            return new UserDto
            {
                UserName = userName,
                FullName = user.FullName,
                Token = token,
                ValidTo = validTo
            };
        }

        public async Task<(string FullName, string Email, string Token)> GenerateResetPasswordAsync(string email)
        {
            var user = await _userRepository.All.FirstOrDefaultAsync(s => s.Email == email);
            if (user == null)
            {
                return (null, null, null);
            }

            var validTo = DateTime.UtcNow.AddHours(24);
            var token = _tokenGenerator.GeneratePasswordResetToken(user.FullName, validTo);
            await SaveResetPasswordTokenAsync(user.Id, token, validTo);
            _jwtTokenRepository.DeleteByUserId(user.Id);
            await _jwtTokenRepository.SaveAsync();
            _logger.LogInformation("Successfully generated reset token for user: {User}", user.FullName);
            return (user.FullName, user.Email, token);
        }

        public async Task DeAuthenticateAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return;
            }

            var token = await GetCurrentTokenAsync();
            if (token == null)
            {
                return;
            }

            _logger.LogInformation($"De-authenticated user: {user.FullName}");

            _jwtTokenRepository.DeleteByUserId(user.Id);
            await _jwtTokenRepository.SaveAsync();
        }

        public async Task<bool> IsValidCurrentTokenAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return false;
            }

            var token = await GetCurrentTokenAsync();
            if (token == null)
            {
                return false;
            }

            var jwtToken = _jwtTokenRepository.GetByUserIdAndToken(user.Id, token);

            return jwtToken != null &&
                   !jwtToken.Cancelled;
        }

        public async Task<bool> IsValidCurrentResetPasswordTokenAsync(string resetPasswordToken)
        {
            var resetPasswordEntity = await _resetPasswordTokenRepository.GetByTokenAsync(resetPasswordToken);
            return resetPasswordEntity != null && !resetPasswordEntity.Expired && resetPasswordEntity.ValidTo >= DateTime.UtcNow;
        }

        public async Task<User> GetAuthenticatedUserAsync() =>
            await GetCurrentUserAsync();

        private async Task<User> GetCurrentUserAsync()
        {
            var userName = GetCurrentUserName();
            if (userName == null)
            {
                return null;
            }

            return await _userRepository.GetByUsernameAsync(userName);
        }

        private async Task SaveTokenAsync(Guid userId, string tokenValue, DateTime validTo, bool cancelled = false)
        {
            var token = CreateToken(userId, tokenValue);
            token.Cancelled = cancelled;
            token.ValidTo = validTo;

            _jwtTokenRepository.Add(token);
            await _jwtTokenRepository.SaveAsync();
        }

        private async Task SaveResetPasswordTokenAsync(Guid userId, string tokenValue, DateTime validTo)
        {
            var tokenDbEntry = new ResetPasswordToken
            {
                Expired = false,
                TokenString = tokenValue,
                UserId = userId,
                ValidTo = validTo
            };
            _jwtTokenRepository.DeleteByUserId(userId);
            await _jwtTokenRepository.SaveAsync();
            _resetPasswordTokenRepository.Add(tokenDbEntry);
            await _resetPasswordTokenRepository.SaveAsync();
        }

        private JwtToken CreateToken(Guid userId, string tokenValue)
        {
            var token = _jwtTokenRepository.GetByUserId(userId);
            if (token == null)
            {
                return new JwtToken { UserId = userId, Token = tokenValue };
            }

            token.Token = tokenValue;
            return token;
        }


        public async Task<bool> ResetUserPassword(string token, string password)
        {
            var tokenEntry = await _resetPasswordTokenRepository.GetByTokenAsync(token);
            if (tokenEntry == null || tokenEntry.Expired || tokenEntry.ValidTo < DateTime.UtcNow)
            {
                return false;
            }

            var user = await _userRepository.GetByIdAsync(tokenEntry.UserId);
            if (user == null)
            {
                return false;
            }

            _resetPasswordTokenRepository.Delete(tokenEntry);
            return await _userService.ResetUserPassword(user, password);
        }


        private string GetCurrentUserName() =>
            _httpContextAccessor.HttpContext?.User.Identity?.Name;

        private async Task<string> GetCurrentTokenAsync() =>
            await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
    }
}
