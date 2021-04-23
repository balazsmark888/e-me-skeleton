using System;
using System.Linq;
using System.Threading.Tasks;
using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace e_me.Model.Repositories
{
    public class UserDocumentRepository : BaseRepository<UserDocument>, IUserDocumentRepository
    {
        public UserDocumentRepository(ApplicationDbContext context, ApplicationUserContext userContext)
            : base(context, userContext)
        {
        }

        public IQueryable<UserDocument> GetByUserId(Guid userId)
        {
            return All.Where(p => p.UserId == userId);
        }

        public async Task<UserDocument> GetByUserIdAndTemplateId(Guid userId, Guid templateId)
        {
            return await All.FirstOrDefaultAsync(p => p.UserId == userId && p.DocumentTemplateId == templateId);
        }

        public async Task<bool> IsValidUserDocumentId(Guid userDocumentId)
        {
            var document = await GetByIdAsync(userDocumentId);
            return document != null;
        }
    }

    public interface IUserDocumentRepository : IBaseRepository<UserDocument>
    {
        IQueryable<UserDocument> GetByUserId(Guid userId);

        Task<UserDocument> GetByUserIdAndTemplateId(Guid userId, Guid templateId);

        Task<bool> IsValidUserDocumentId(Guid userDocumentId);
    }
}
