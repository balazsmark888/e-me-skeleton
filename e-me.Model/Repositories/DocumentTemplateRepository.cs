using System;
using System.Threading.Tasks;
using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace e_me.Model.Repositories
{
    public class DocumentTemplateRepository : BaseRepository<DocumentTemplate>, IDocumentTemplateRepository
    {
        public DocumentTemplateRepository(ApplicationDbContext context, ApplicationUserContext userContext)
            : base(context, userContext)
        {
        }
        public async Task<DocumentTemplate> GetByTypeAsync(Guid typeId)
        {
            return await All.FirstOrDefaultAsync(p => p.DocumentTypeId.Equals(typeId));
        }
    }

    public interface IDocumentTemplateRepository : IBaseRepository<DocumentTemplate>
    {
        Task<DocumentTemplate> GetByTypeAsync(Guid typeId);
    }
}
