using System;
using System.Net.Http;
using System.Threading.Tasks;
using e_me.Business.DTOs;
using e_me.Model.Models;
using e_me.Model.Repositories;
using e_me.Mvc.Auth.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace e_me.Mvc.Auth
{
    /// <summary>
    /// Service implementation for auth-related operations.
    /// </summary>
    public class AuthService : IAuthService
    {
        public readonly AuthSettings AuthSettings;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenRepository _jwtTokenRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository,
                     IJwtTokenRepository jwtTokenRepository,
                     ITokenGenerator tokenGenerator,
                     IHttpContextAccessor httpContextAccessor,
                     IOptions<AuthSettings> authSettings,
                     ILogger<AuthService> logger)
        {
            AuthSettings = authSettings.Value;
            _userRepository = userRepository;
            _jwtTokenRepository = jwtTokenRepository;
            _tokenGenerator = tokenGenerator;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="userName">User's login name.</param>
        /// <param name="password">User's password.</param>
        /// <returns></returns>
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

            _logger.LogInformation($"Successfully authenticated user:{userName} as {role}");

            return new UserDto
            {
                UserName = userName,
                FullName = user.FullName,
                Token = token,
                ValidTo = validTo
            };
        }

        /// <summary>
        /// De-authenticates the user.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Checks whether the current user's jwt token is valid or not.
        /// </summary>
        /// <returns>True if the token is valid, false otherwise.</returns>
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

        /// <summary>
        /// Gets the authenticated user.
        /// </summary>
        /// <returns>The current user</returns>
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

            await _jwtTokenRepository.AddAsync(token);
            await _jwtTokenRepository.SaveAsync();
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

        private string GetCurrentUserName() =>
            _httpContextAccessor.HttpContext?.User.Identity?.Name;

        private async Task<string> GetCurrentTokenAsync() =>
            await (_httpContextAccessor.HttpContext ?? throw new HttpRequestException("No http context found!")).GetTokenAsync("access_token");
    }
}
