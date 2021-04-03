using System;
using System.Linq;
using System.Threading.Tasks;
using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<UserEcdhKeyInformation> GetByUserIdAsync(Guid userId)
        {
            return await All.FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }

    public interface IUserEcdhKeyInformationRepository : IBaseRepository<UserEcdhKeyInformation>
    {
        void DeleteByUserId(Guid userId);

        Task<UserEcdhKeyInformation> GetByUserIdAsync(Guid userId);
    }
}
