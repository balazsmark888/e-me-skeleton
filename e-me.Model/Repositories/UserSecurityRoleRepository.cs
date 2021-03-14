using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;

namespace e_me.Model.Repositories
{
    public class UserSecurityRoleRepository : BaseRepository<UserSecurityRole>, IUserSecurityRoleRepository
    {
        public UserSecurityRoleRepository(ApplicationDbContext context, ApplicationUserContext userContext)
            : base(context, userContext)
        {
        }
    }

    public interface IUserSecurityRoleRepository : IBaseRepository<UserSecurityRole>
    {
    }
}
