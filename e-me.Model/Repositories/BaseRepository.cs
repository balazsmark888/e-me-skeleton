using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using e_me.Core.Application;
using e_me.Model.DBContext;
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

    public abstract class BaseRepository<TEntity> : BaseRepository, IBaseRepository<TEntity> where TEntity : Models.BaseModel
    {
        protected BaseRepository(ApplicationDbContext context, ApplicationUserContext userContext)
            : base(context, userContext)
        {
        }

        public IQueryable<TEntity> All =>
            Context.Set<TEntity>();

        public IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties) =>
            includeProperties.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(Context.Set<TEntity>(),
                (current, includeProperty) => current.Include(includeProperty));

        public virtual void Insert(TEntity entity) =>
            Context.Set<TEntity>().Add(entity);

        public virtual async Task InsertAsync(TEntity entity) =>
            await Context.Set<TEntity>().AddAsync(entity);

        public virtual void Update(TEntity entity) =>
            Context.Entry(entity).State = EntityState.Modified;

        public void InsertOrUpdate(TEntity entity)
        {
            if (entity.Id == default)
            {
                Insert(entity);
            }
            else
            {
                Update(entity);
            }
        }

        public async Task InsertOrUpdateAsync(TEntity entity)
        {
            if (entity.Id == default)
            {
                await InsertAsync(entity);
            }
            else
            {
                Update(entity);
            }
        }

        public virtual void Delete(TEntity entity) =>
            Context.Set<TEntity>().Remove(entity);

        public virtual void DeleteById(Guid id) =>
            Context.Set<TEntity>().Remove(GetById(id));

        public virtual TEntity GetById(Guid id) =>
            Context.Set<TEntity>().Find(id);

        public virtual async Task<TEntity> GetByIdAsync(Guid id) =>
            await Context.Set<TEntity>().FindAsync(id);

        public virtual async Task<int> SaveAsync()
        {
            Context.ChangeTracker.AutoDetectChangesEnabled = false;
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
