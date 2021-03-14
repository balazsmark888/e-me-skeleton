using System;
using System.Linq;
using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;

namespace e_me.Model.Repositories
{
    public class JwtTokenRepository : BaseRepository<JwtToken>, IJwtTokenRepository
    {
        public JwtTokenRepository(ApplicationDbContext context, ApplicationUserContext userContext)
            : base(context, userContext)
        {
        }

        public JwtToken GetByUserId(Guid userId)
        {
            return All.FirstOrDefault(t => t.UserId.Equals(userId));
        }

        public JwtToken GetByUserIdAndToken(Guid userId, string token)
        {
            return All.FirstOrDefault(t => t.UserId.Equals(userId) && t.Token.Equals(token));
        }

        public void DeleteByUserId(Guid userId)
        {
            Context.Set<JwtToken>().RemoveRange(Context.Set<JwtToken>().Where(t => t.UserId.Equals(userId)));
        }
    }

    public interface IJwtTokenRepository : IBaseRepository<JwtToken>
    {
        public JwtToken GetByUserId(Guid userId);

        public JwtToken GetByUserIdAndToken(Guid userId, string token);

        public void DeleteByUserId(Guid userId);
    }
}
