using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace e_me.Model.Repositories
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : Models.BaseModel
    {
        void Insert(TEntity entity);

        Task InsertAsync(TEntity entity);

        void Update(TEntity entity);

        void InsertOrUpdate(TEntity entity);

        Task InsertOrUpdateAsync(TEntity entity);

        void Delete(TEntity entity);

        void DeleteById(Guid id);

        IQueryable<TEntity> All { get; }

        IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity GetById(Guid id);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<int> SaveAsync();
    }
}
