using System;
using System.Linq;
using System.Threading.Tasks;
using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace e_me.Model.Repositories
{
    public class BaseRepository
    {
        protected ApplicationDbContext Context { get; }

        protected ApplicationUserContext ApplicationUserContext { get; }

        public BaseRepository(ApplicationDbContext context, ApplicationUserContext userContext)
        {
            Context = context;
            ApplicationUserContext = userContext;
        }
    }

    public abstract class BaseRepository<TEntity> : BaseRepository, IDisposable where TEntity : BaseModel
    {
        protected BaseRepository(ApplicationDbContext context, ApplicationUserContext userContext) : base(context, userContext)
        {
        }

        public IQueryable<TEntity> All => Context.Set<TEntity>();

        public virtual void Add(TEntity entity) =>
            Context.Set<TEntity>().Add(entity);

        public virtual void Update(TEntity entity) =>
            Context.Entry(entity).State = EntityState.Modified;

        public virtual void Delete(TEntity tenant) =>
            Context.Set<TEntity>().Remove(tenant);

        public virtual async Task SaveAsync()
        {
            Context.ChangeTracker.AutoDetectChangesEnabled = false;
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
