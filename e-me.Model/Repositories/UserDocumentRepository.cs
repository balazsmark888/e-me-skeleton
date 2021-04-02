using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;

namespace e_me.Model.Repositories
{
    public class UserDocumentRepository : BaseRepository<UserDocument>, IUserDocumentRepository
    {
        public UserDocumentRepository(ApplicationDbContext context, ApplicationUserContext userContext)
            : base(context, userContext)
        {
        }
    }

    public interface IUserDocumentRepository : IBaseRepository<UserDocument>
    {
    }
}
