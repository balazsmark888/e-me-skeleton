using System;
using System.Linq;
using System.Threading.Tasks;
using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace e_me.Model.Repositories
{
    public class ResetPasswordTokenRepository : BaseRepository<ResetPasswordToken>, IResetPasswordTokenRepository
    {
        public ResetPasswordTokenRepository(ApplicationDbContext context, ApplicationUserContext userContext)
            : base(context, userContext)
        {
        }

        public async Task<ResetPasswordToken> GetByTokenAsync(string token)
        {
            return await All.FirstOrDefaultAsync(t => t.TokenString.Equals(token));
        }

        public void DeleteByUserId(Guid userId)
        {
            Context.ResetPasswordTokens.RemoveRange(All.Where(p => p.UserId.Equals(userId)));
        }
    }

    public interface IResetPasswordTokenRepository : IBaseRepository<ResetPasswordToken>
    {
        Task<ResetPasswordToken> GetByTokenAsync(string token);

        void DeleteByUserId(Guid userId);
    }
}
