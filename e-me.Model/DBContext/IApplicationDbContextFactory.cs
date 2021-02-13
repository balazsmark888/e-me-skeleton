using Microsoft.EntityFrameworkCore;

namespace e_me.Model.DBContext
{
    public interface IApplicationDbContextFactory
    {
        public DbContext GetDbContext();
    }
}
