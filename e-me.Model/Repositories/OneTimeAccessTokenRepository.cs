using System;
using System.Threading.Tasks;
using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace e_me.Model.Repositories
{
    public class OneTimeAccessTokenRepository : BaseRepository<OneTimeAccessToken>, IOneTimeAccessTokenRepository
    {
        public OneTimeAccessTokenRepository(ApplicationDbContext context, ApplicationUserContext userContext)
            : base(context, userContext)
        {
        }

        public async Task<OneTimeAccessToken> FindByUserDocumentIdAsync(Guid userDocumentId)
        {
            return await All.FirstOrDefaultAsync(p => p.UserDocumentId == userDocumentId);
        }
    }

    public interface IOneTimeAccessTokenRepository : IBaseRepository<OneTimeAccessToken>
    {
        Task<OneTimeAccessToken> FindByUserDocumentIdAsync(Guid userDocumentId);
    }
}
