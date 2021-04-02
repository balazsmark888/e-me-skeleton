using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;

namespace e_me.Model.Repositories
{
    public class UserDetailsRepository : BaseRepository<UserDetail>, IUserDetailsRepository
    {
        public UserDetailsRepository(ApplicationDbContext context, ApplicationUserContext userContext)
            : base(context, userContext)
        {
        }
    }

    public interface IUserDetailsRepository : IBaseRepository<UserDetail>
    {
    }
}
