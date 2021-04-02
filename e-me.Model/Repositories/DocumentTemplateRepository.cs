using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;

namespace e_me.Model.Repositories
{
    public class DocumentTemplateRepository : BaseRepository<DocumentTemplate>, IDocumentTemplateRepository
    {
        public DocumentTemplateRepository(ApplicationDbContext context, ApplicationUserContext userContext)
            : base(context, userContext)
        {
        }
    }

    public interface IDocumentTemplateRepository : IBaseRepository<DocumentTemplate>
    {
    }
}
