using System;
using System.Linq;
using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;

namespace e_me.Model.Repositories
{
    public class UserEcdhKeyInformationRepository : BaseRepository<UserEcdhKeyInformation>, IUserEcdhKeyInformationRepository
    {
        public UserEcdhKeyInformationRepository(ApplicationDbContext context, ApplicationUserContext userContext)
            : base(context, userContext)
        {
        }

        public void DeleteByUserId(Guid userId)
        {
            Context.Set<UserEcdhKeyInformation>().RemoveRange(Context.Set<UserEcdhKeyInformation>().Where(t => t.UserId.Equals(userId)));
        }
    }

    public interface IUserEcdhKeyInformationRepository : IBaseRepository<UserEcdhKeyInformation>
    {
        public void DeleteByUserId(Guid userId);
    }
}
