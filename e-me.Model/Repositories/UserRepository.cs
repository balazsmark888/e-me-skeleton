using System;
using System.Linq;
using System.Threading.Tasks;
using e_me.Core.Application;
using e_me.Core.Helpers;
using e_me.Model.DBContext;
using e_me.Model.Models;
using e_me.Model.Resources;
using Microsoft.EntityFrameworkCore;
using static e_me.Core.Helpers.Cryptography;

namespace e_me.Model.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IUserSecurityRoleRepository _userSecurityRoleRepository;
        private readonly ISecurityRoleRepository _securityRoleRepository;

        public UserRepository(ApplicationDbContext context,
            ApplicationUserContext userContext,
            ISecurityRoleRepository securityRoleRepository,
            IUserSecurityRoleRepository userSecurityRoleRepository)
            : base(context, userContext)
        {
            _securityRoleRepository = securityRoleRepository;
            _userSecurityRoleRepository = userSecurityRoleRepository;
        }

        public User CurrentUser => GetByUsername(ApplicationUserContext.CurrentUserName);

        public Task<User> CurrentUserAsync() => GetByUsernameAsync(ApplicationUserContext.CurrentUserName);

        public async Task<string> GetUserRoleAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception(ModelResources.msgUserNotFound);
            }
            var userSecurityRole = _userSecurityRoleRepository.AllIncluding(s => s.SecurityRole).FirstOrDefault(s => s.Id == id);
            if (userSecurityRole == null)
            {
                throw new Exception("User does not have a SecurityRole attached!");
            }
            return userSecurityRole.SecurityRole.Name;
        }

        public User GetByUsernameAndPassword(string name, string password)
        {
            var users = All.Where(p => p.LoginName.Trim() == name.Trim()).ToList();
            if (users.Any() && users.Count == 1)
            {
                try
                {
                    if (string.CompareOrdinal(password, Decrypt(users[0].Password)) == 0)
                    {
                        return users[0];
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            else
            {
                var user = GetByEmail(name);
                if (user == null) return null;
                try
                {
                    return string.CompareOrdinal(password, Decrypt(user.Password)) != 0 ? null : user;
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return null;
        }

        public User GetByUsername(string username)
        {
            var user = All.FirstOrDefault(p => p.LoginName.Trim() == username.Trim());
            if (user == null) return null;
            {
                var userSecurityRole = _userSecurityRoleRepository.AllIncluding(p => p.SecurityRole).FirstOrDefault(p => p.UserId == user.Id);

                user.SecurityRoleId = userSecurityRole?.SecurityRole.Id ?? default;

                user.SecurityRole = _securityRoleRepository.GetById(user.SecurityRoleId);
            }

            return user;
        }

        public User GetByEmail(string email)
        {
            return All.FirstOrDefault(p => p.Email.ToLower().Trim() == email.ToLower().Trim());
        }

        public User CreateEmptyUser()
        {
            if (CurrentUser != null)
            {
                var password = PasswordGeneration.GenerateRandomPassword();
                return new User
                {
                    ChangePasswordNextLogon = true,
                    Password = password,
                    PasswordConfirmation = password,
                };
            }

            return new User();
        }

        public async Task<User> CreateEmptyUserAsync()
        {
            var currentUser = await CurrentUserAsync();
            if (currentUser != null)
            {
                var password = PasswordGeneration.GenerateRandomPassword();
                return new User
                {
                    ChangePasswordNextLogon = true,
                    Password = password,
                    PasswordConfirmation = password,
                };
            }

            return new User();
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var user = await All.FirstOrDefaultAsync(p => p.LoginName.Trim() == username.Trim());
            if (user == null) return null;
            var userSecurityRole = await _userSecurityRoleRepository.AllIncluding(p => p.SecurityRole).Where(p => p.UserId == user.Id).FirstOrDefaultAsync();

            user.SecurityRoleId = userSecurityRole?.SecurityRole.Id ?? default;

            user.SecurityRole = await _securityRoleRepository.GetByIdAsync(user.SecurityRoleId);
            return user;
        }
    }

    public interface IUserRepository : IBaseRepository<User>
    {
        User CurrentUser { get; }

        Task<User> CurrentUserAsync();

        Task<string> GetUserRoleAsync(Guid id);

        User GetByUsernameAndPassword(string name, string password);

        User GetByUsername(string username);

        Task<User> GetByUsernameAsync(string username);

        User GetByEmail(string email);

        User CreateEmptyUser();

        Task<User> CreateEmptyUserAsync();
    }
}
