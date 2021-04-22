using System;
using System.Threading.Tasks;
using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace e_me.Model.Repositories
{
    public class DocumentTypeRepository : BaseRepository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(ApplicationDbContext context, ApplicationUserContext userContext) 
            : base(context, userContext)
        {
        }
    }

    public interface IDocumentTypeRepository : IBaseRepository<DocumentType>
    {
    }
}
