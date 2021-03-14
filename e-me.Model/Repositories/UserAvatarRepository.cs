using System;
using System.Linq;
using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;

namespace e_me.Model.Repositories
{
    public class UserAvatarRepository : BaseRepository<UserAvatar>, IUserAvatarRepository
    {
        public UserAvatarRepository(ApplicationDbContext context, ApplicationUserContext userContext)
            : base(context, userContext)
        {
        }

        public UserAvatar GetByUserId(Guid userId)
        {
            return All.FirstOrDefault(p => p.UserId.Equals(userId));
        }
    }

    public interface IUserAvatarRepository : IBaseRepository<UserAvatar>
    {
        UserAvatar GetByUserId(Guid userId);
    }
}
