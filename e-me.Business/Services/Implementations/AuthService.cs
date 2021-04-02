using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using e_me.Business.DTOs;
using e_me.Business.Services.Interfaces;
using e_me.Core.Communication;
using e_me.Model.Models;
using e_me.Model.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace e_me.Business.Services.Implementations
{
    /// <summary>
    /// Service implementation for auth-related operations.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly AuthSettings _authSettings;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenRepository _jwtTokenRepository;
        private readonly IUserEcdhKeyInformationRepository _userEcdhKeyInformationRepository;
        private readonly ITokenGeneratorService _tokenGeneratorService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthService> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor that initializes services and repositories needed for auth-related operations using DI.
        /// </summary>
        /// <param name="userRepository">Data-access layer object for Users.</param>
        /// <param name="jwtTokenRepository">Data-access layer object for JwtTokens</param>
        /// <param name="tokenGeneratorService">Token Generator service.</param>
        /// <param name="httpContextAccessor">Http context accessor service.</param>
        /// <param name="authSettings">Authentication settings object.</param>
        /// <param name="logger">Logger object.</param>
        /// <param name="mapper">Auto-mapper object.</param>
        /// <param name="userEcdhKeyInformationRepository">Data-access layer object for UserEcdhKeyInformation.</param>
        public AuthService(IUserRepository userRepository,
                     IJwtTokenRepository jwtTokenRepository,
                     ITokenGeneratorService tokenGeneratorService,
                     IHttpContextAccessor httpContextAccessor,
                     IOptions<AuthSettings> authSettings,
                     ILogger<AuthService> logger,
                     IMapper mapper,
                     IUserEcdhKeyInformationRepository userEcdhKeyInformationRepository)
        {
            _authSettings = authSettings.Value;
            _userRepository = userRepository;
            _jwtTokenRepository = jwtTokenRepository;
            _tokenGeneratorService = tokenGeneratorService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _mapper = mapper;
            _userEcdhKeyInformationRepository = userEcdhKeyInformationRepository;
        }

        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="authDto">Contains auth information</param>
        /// <returns></returns>
        public async Task<UserDto> AuthenticateAsync(AuthDto authDto)
        {
            var loginName = authDto.LoginName;
            var password = authDto.Password;
            var clientPublicKey = authDto.PublicKey;

            var user = _userRepository.GetByUsernameAndPassword(loginName, password);
            if (user == null)
            {
                return null;
            }

            var role = await _userRepository.GetUserRoleAsync(user.Id);
            var validTo = DateTime.UtcNow + _authSettings.TokenLifeTimeDuration;
            var token = _tokenGeneratorService.Generate(loginName, validTo, role);
            await SaveTokenAsync(user.Id, token, validTo);

            _logger.LogInformation($"Successfully authenticated user:{loginName} as {role}");

            var keyStore = new EcdhKeyStore(clientPublicKey);
            var userKeyInfo = _mapper.Map<UserEcdhKeyInformation>(keyStore);
            userKeyInfo.UserId = user.Id;
            _userEcdhKeyInformationRepository.DeleteByUserId(user.Id);
            await _userEcdhKeyInformationRepository.AddAsync(userKeyInfo);
            await _userEcdhKeyInformationRepository.SaveAsync();

            return new UserDto
            {
                UserName = loginName,
                FullName = user.FullName,
                Token = token,
                ValidTo = validTo,
                PublicKey = keyStore.PublicKey.ToByteArray()
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
            _userEcdhKeyInformationRepository.DeleteByUserId(user.Id);
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
