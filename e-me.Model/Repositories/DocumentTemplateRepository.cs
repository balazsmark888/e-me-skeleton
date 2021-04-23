using System;
using System.Linq;
using System.Threading.Tasks;
using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace e_me.Model.Repositories
{
    public class DocumentTemplateRepository : BaseRepository<DocumentTemplate>, IDocumentTemplateRepository
    {
        private readonly IUserDocumentRepository _userDocumentRepository;

        public DocumentTemplateRepository(ApplicationDbContext context, ApplicationUserContext userContext, IUserDocumentRepository userDocumentRepository)
            : base(context, userContext)
        {
            _userDocumentRepository = userDocumentRepository;
        }
        public async Task<DocumentTemplate> GetByTypeAsync(Guid typeId)
        {
            return await All.FirstOrDefaultAsync(p => p.DocumentTypeId.Equals(typeId));
        }

        public async Task<IQueryable<DocumentTemplate>> GetAvailableAsync(Guid userId)
        {
            var existingTemplates = await _userDocumentRepository.GetByUserId(userId).Select(p => p.DocumentTemplateId).ToListAsync();
            var available = All.Where(p => !existingTemplates.Contains(p.DocumentTypeId));
            return available;
        }
    }

    public interface IDocumentTemplateRepository : IBaseRepository<DocumentTemplate>
    {
        Task<DocumentTemplate> GetByTypeAsync(Guid typeId);

        Task<IQueryable<DocumentTemplate>> GetAvailableAsync(Guid userId);
    }
}
