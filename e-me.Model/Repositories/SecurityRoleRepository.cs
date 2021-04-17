using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;

namespace e_me.Model.Repositories
{
    public class SecurityRoleRepository : BaseRepository<SecurityRole>, ISecurityRoleRepository
    {
        public SecurityRoleRepository(ApplicationDbContext context, ApplicationUserContext userContext)
            : base(context, userContext)
        {
        }
    }

    public interface ISecurityRoleRepository : IBaseRepository<SecurityRole>
    {
    }
}
