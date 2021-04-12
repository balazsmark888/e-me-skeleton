using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using e_me.Business.Services.Interfaces;
using e_me.Core;
using e_me.Core.Helpers;
using e_me.Model.Models;
using e_me.Model.Repositories;
using e_me.Shared.DTOs.SecurityRole;
using e_me.Shared.DTOs.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace e_me.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSecurityRoleRepository _userSecurityRoleRepository;
        private readonly IUserAvatarRepository _userAvatarRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly ISecurityRoleRepository _securityRoleRepository;
        private readonly IJwtTokenRepository _jwtTokenRepository;

        public UserService(IUserRepository userRepository,
                     IUserSecurityRoleRepository userSecurityRoleRepository,
                     IUserAvatarRepository userAvatarRepository,
                     ILogger<UserService> logger,
                     IMapper mapper,
                     ISecurityRoleRepository securityRoleRepository,
                     IJwtTokenRepository jwtTokenRepository)
        {
            _userRepository = userRepository;
            _userSecurityRoleRepository = userSecurityRoleRepository;
            _userAvatarRepository = userAvatarRepository;
            _logger = logger;
            _mapper = mapper;
            _securityRoleRepository = securityRoleRepository;
            _jwtTokenRepository = jwtTokenRepository;
        }

        public IQueryable<User> All => _userRepository.All;

        public async Task<User> CreateAsync() => await _userRepository.CreateEmptyUserAsync();

        public async Task<bool> ChangePasswordLoggedUserAsync(User user)
        {
            var oldUser = await _userRepository.GetByIdAsync(user.Id);
            if (oldUser == null)
            {
                throw new ApplicationException(Resources.msgUserNotFound);
            }

            user.CreationDate = oldUser.CreationDate;
            if (!string.Equals(user.Password, user.PasswordConfirmation))
            {
                throw new ApplicationException(Resources.msgPasswordConfirmIncorrect);
            }

            if (string.Equals(user.Password, user.OldPassword))
            {
                throw new ApplicationException(Resources.msgOldAndNewPasswordsShouldDiffer);
            }

            var oldPassword = Cryptography.Decrypt(oldUser.Password);
            if (!string.Equals(oldPassword, user.OldPassword))
            {
                throw new ApplicationException(Resources.msgOldPasswordInputIncorrect);
            }

            if (string.IsNullOrWhiteSpace(user.OldPassword))
            {
                throw new ApplicationException(Resources.msgOldPasswordMandatory);
            }

            if (string.IsNullOrWhiteSpace(user.PasswordConfirmation))
            {
                throw new ApplicationException(Resources.msgPasswordConfirmMandatory);
            }

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                oldUser.Password = Cryptography.GetEncryptedPassword(user.Password);
            }
            else
            {
                throw new ApplicationException(Resources.msgPasswordMandatory);
            }

            await _userRepository.InsertOrUpdateAsync(oldUser);
            await _userRepository.SaveAsync();
            return true;
        }

        public async Task<string> ChangePasswordAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return string.Empty;
            }

            user.Password = string.Empty;
            user.PasswordConfirmation = string.Empty;
            user.ChangePasswordNextLogon = true;
            _jwtTokenRepository.DeleteByUserId(id);
            await _userRepository.InsertOrUpdateAsync(user);
            await _userRepository.SaveAsync();
            return user.Email;
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(oldPassword))
            {
                throw new ApplicationException(Resources.msgPasswordConfirmMandatory);
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var encryptedOldPassword = Cryptography.Encrypt(oldPassword);
            if (!string.Equals(user.Password, encryptedOldPassword))
            {
                throw new ApplicationException(Resources.msgPasswordConfirmMandatory);
            }

            user.Password = Cryptography.GetEncryptedPassword(newPassword);

            await _userRepository.InsertOrUpdateAsync(user);
            await _userRepository.SaveAsync();
            return true;
        }

        public async Task<bool> ResetUserPassword(User user, string newPassword)
        {
            if (user == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                return false;
            }
            user.Password = Cryptography.GetEncryptedPassword(newPassword);
            await _userRepository.InsertOrUpdateAsync(user);
            await _userRepository.SaveAsync();
            return true;
        }

        public async Task<bool> ChangePasswordAsync(User user)
        {
            var oldUser = await _userRepository.GetByUsernameAsync(user.LoginName);
            if (oldUser == null)
            {
                return false;
            }

            user.CreationDate = oldUser.CreationDate;
            if (!string.Equals(user.Password, user.PasswordConfirmation))
            {
                throw new ApplicationException(Resources.msgPasswordConfirmIncorrect);
            }

            if (string.IsNullOrWhiteSpace(user.PasswordConfirmation))
            {
                throw new ApplicationException(Resources.msgPasswordConfirmMandatory);
            }

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                oldUser.Password = Cryptography.GetEncryptedPassword(user.Password);
            }
            else
            {
                throw new ApplicationException(Resources.msgPasswordMandatory);
            }

            await _userRepository.InsertOrUpdateAsync(oldUser);
            await _userRepository.SaveAsync();
            return true;

        }

        public async Task<bool> UpdateAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            user = await _userRepository.GetByUsernameAsync(user.LoginName);
            user.PasswordConfirmation = user.Password;
            return true;
        }

        public async Task<User> CreateAsync(User user, string password)
        {
            if (user == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ApplicationException(Resources.msgPasswordMandatory);
            }

            user.CreationDate = DateTime.Now;
            user.Status = true;

            var encryptedPassword = Cryptography.GetEncryptedPassword(password);
            user.Password = encryptedPassword;

            await _userRepository.InsertOrUpdateAsync(user);
            await _userRepository.SaveAsync();

            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            var isNewUser = user.Id == default;
            var oldUser = await _userRepository.GetByIdAsync(user.Id);
            if (!string.IsNullOrWhiteSpace(user.Password) && user.Id == default)
            {
                user.Password = Cryptography.GetEncryptedPassword(user.Password);
            }

            if (!string.IsNullOrWhiteSpace(user.Password) && oldUser != null)
            {
                if (!string.Equals(oldUser.Password, user.Password))
                {
                    user.Password = Cryptography.GetEncryptedPassword(user.Password);
                }
            }

            if (oldUser != null)
            {
                user.CreationDate = oldUser.CreationDate;
            }

            await _userRepository.InsertOrUpdateAsync(user);
            var userId = await _userRepository.SaveAsync();
            if (isNewUser)
            {
                _logger.LogInformation("Created user:{LoginName} with id:{UserId} by {CurrentUserName}", user.LoginName, userId, _userRepository.CurrentUser.LoginName);
            }
            else
            {
                if (user.ChangePasswordNextLogon)
                {
                    _logger.LogInformation("Password was reset for user:{LoginName} by {CurrentUserName}", user.LoginName, _userRepository.CurrentUser.LoginName);
                }
            }

            return _userRepository.CurrentUser;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            var userSecurityRoles = _userSecurityRoleRepository.AllIncluding(s => s.SecurityRole).Where(p => p.UserId == id);
            if (userSecurityRoles.Any())
            {
                if (Enumerable.Any(userSecurityRoles, userSecurityRole => userSecurityRole.SecurityRole.SecurityType == (int)Enums.SecurityType.AppAdministrator))
                {
                    throw new ApplicationException(Resources.msgAdminUserCannotBeDeleted);
                }
            }

            _logger.LogInformation("User:{LoginName} was deleted by {CurrentUserName}", user.LoginName, _userRepository.CurrentUser.LoginName);

            _jwtTokenRepository.DeleteByUserId(id);
            _userRepository.DeleteById(id);
            await _userRepository.SaveAsync();
            return true;
        }

        public async Task<byte[]> ChangeAvatarAsync(byte[] img, Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            var userAvatar = _userAvatarRepository.GetByUserId(user.Id);
            if (userAvatar != null)
            {
                userAvatar.Avatar = img;
            }
            else
            {
                userAvatar = new UserAvatar
                {
                    UserId = user.Id,
                    Avatar = img
                };
            }

            await _userAvatarRepository.InsertOrUpdateAsync(userAvatar);
            await _userAvatarRepository.SaveAsync();

            return userAvatar.Avatar;
        }

        public async Task<byte[]> GetAvatarAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            var userAvatar = _userAvatarRepository.GetByUserId(user.Id);
            if (userAvatar != null && userAvatar.HasAvatar)
            {
                return userAvatar.Avatar;
            }
            return null;
        }

        public async Task<UserProfileDto> GetUserDtoAsync(Guid userId)
        {
            var data = await All.FirstOrDefaultAsync(s => s.Id == userId);
            if (data == null)
            {
                throw new Exception("UserId not found!");
            }
            var userRole = await _userSecurityRoleRepository.All.Include(s => s.SecurityRole).FirstOrDefaultAsync(s => s.UserId == userId);
            var mappedUser = _mapper.Map<UserProfileDto>(data);
            if (userRole != null)
            {
                mappedUser.SecurityRoleDto = new SecurityRoleDto
                {
                    Name = userRole.SecurityRole.Name,
                    SecurityRoleId = userRole.SecurityRoleId
                };
            }
            return mappedUser;
        }

        public async Task<bool> UpdateUserAsync(UserProfileDto userProfileDto)
        {
            var user = await All.FirstOrDefaultAsync(s => s.Id == userProfileDto.UserId);
            if (user == null)
            {
                throw new Exception("User not found!");
            }

            if (user.LoginName != userProfileDto.LoginName)
            {
                user.LoginName = userProfileDto.LoginName;
            }
            if (user.FullName != userProfileDto.FullName)
            {
                user.FullName = userProfileDto.FullName;
            }
            if (user.Email != userProfileDto.Email)
            {
                user.Email = userProfileDto.Email;
            }

            await UpdateAsync(user);
            if (userProfileDto.SecurityRoleDto != null)
            {
                var securityRole = await _securityRoleRepository.All.FirstOrDefaultAsync(s => s.Id == userProfileDto.SecurityRoleDto.SecurityRoleId);
                if (securityRole == null)
                {
                    throw new Exception("Security role does not exist!");
                }
                var userRole = await _userSecurityRoleRepository.All.FirstOrDefaultAsync(s => s.UserId == userProfileDto.UserId);

                if (userRole == null)
                {
                    userRole = new UserSecurityRole()
                    {
                        UserId = userProfileDto.UserId,
                        SecurityRoleId = userProfileDto.SecurityRoleDto.SecurityRoleId
                    };
                }
                else
                {
                    if (userRole.SecurityRoleId != userProfileDto.SecurityRoleDto.SecurityRoleId)
                    {
                        userRole.SecurityRoleId = userProfileDto.SecurityRoleDto.SecurityRoleId;
                        _jwtTokenRepository.DeleteByUserId(userProfileDto.UserId);
                        await _jwtTokenRepository.SaveAsync();
                    }
                }
                await _userSecurityRoleRepository.InsertOrUpdateAsync(userRole);
                await _userSecurityRoleRepository.SaveAsync();
            }
            return true;
        }

        public async Task<UserProfileDto> CreateUserAsync(UserRegistrationDto userRegistrationDto)
        {
            var existingUserEmail = await All.AnyAsync(s => s.Email == userRegistrationDto.Email.Trim().ToLowerInvariant());
            if (existingUserEmail)
            {
                throw new ApplicationException(Resources.msgEmailAlreadyExists);
            }

            var defaultSecurityRole = _securityRoleRepository.All
                .FirstOrDefault(p => p.SecurityType == (int)Enums.SecurityType.RegularUser);

            if (defaultSecurityRole == null)
            {
                throw new ApplicationException(Resources.msgDefaultSecurityRoleNotFound);
            }

            var userToBeAdded = _mapper.Map<User>(userRegistrationDto);
            userToBeAdded.SecurityRoleId = defaultSecurityRole.Id;

            var user = await CreateAsync(userToBeAdded, userRegistrationDto.Password);
            await SetUserRole(user.Id, defaultSecurityRole);
            var mappedUser = await GetUserDtoAsync(user.Id);
            return mappedUser;
        }

        private async Task SetUserRole(Guid userId, SecurityRole securityRole)
        {
            if (securityRole != null)
            {
                var userRole = await _userSecurityRoleRepository.All.FirstOrDefaultAsync(s => s.UserId == userId);

                if (userRole == null)
                {
                    userRole = new UserSecurityRole()
                    {
                        UserId = userId,
                        SecurityRoleId = securityRole.Id
                    };
                }
                else
                {
                    if (userRole.SecurityRoleId != securityRole.Id)
                    {
                        userRole.SecurityRoleId = securityRole.Id;
                    }
                }
                await _userSecurityRoleRepository.InsertOrUpdateAsync(userRole);
                await _userSecurityRoleRepository.SaveAsync();
            }
        }
    }

}
