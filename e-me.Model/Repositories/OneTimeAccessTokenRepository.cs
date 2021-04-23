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

        public async Task<OneTimeAccessToken> RequestAccessTokenAsync(Guid userDocumentId)
        {
            var existingToken = await FindByUserDocumentIdAsync(userDocumentId);
            if (existingToken != null)
            {
                return null;
            }

            var newToken = new OneTimeAccessToken
            {
                Id = Guid.NewGuid(),
                IsValid = true,
                UserDocumentId = userDocumentId,
                ValidTo = DateTime.Now.AddHours(1)
            };
            await InsertAsync(newToken);
            await SaveAsync();
            return newToken;
        }

        public async Task<OneTimeAccessToken> FindByUserDocumentIdAsync(Guid userDocumentId)
        {
            return await All.FirstOrDefaultAsync(p => p.UserDocumentId == userDocumentId);
        }
    }

    public interface IOneTimeAccessTokenRepository : IBaseRepository<OneTimeAccessToken>
    {
        Task<OneTimeAccessToken> RequestAccessTokenAsync(Guid userDocumentId);
        Task<OneTimeAccessToken> FindByUserDocumentIdAsync(Guid userDocumentId);
    }
}
