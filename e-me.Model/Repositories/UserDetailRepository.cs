using System;
using System.Threading.Tasks;
using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace e_me.Model.Repositories
{
    public class UserDetailRepository : BaseRepository<UserDetail>, IUserDetailRepository
    {
        public UserDetailRepository(ApplicationDbContext context, ApplicationUserContext userContext)
            : base(context, userContext)
        {
        }

        public async Task<UserDetail> GetByUserIdAsync(Guid userId)
        {
            return await All.FirstOrDefaultAsync(p => p.UserId == userId) ?? await CreateAsync(userId);
        }

        public async Task<UserDetail> CreateAsync(Guid userid)
        {
            var userDetail = new UserDetail
            {
                UserId = userid
            };
            await InsertOrUpdateAsync(userDetail);
            await SaveAsync();
            return userDetail;
        }
    }

    public interface IUserDetailRepository : IBaseRepository<UserDetail>
    {
        Task<UserDetail> GetByUserIdAsync(Guid userId);

        Task<UserDetail> CreateAsync(Guid userid);
    }
}
