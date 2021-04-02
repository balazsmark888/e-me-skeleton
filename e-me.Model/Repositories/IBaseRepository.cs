using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using e_me.Model.Models;

namespace e_me.Model.Repositories
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : Models.Model
    {
        void Add(TEntity entity);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void AddOrUpdate(TEntity entity);

        Task AddOrUpdateAsync(TEntity entity);

        void Delete(TEntity entity);

        void DeleteById(Guid id);

        IQueryable<TEntity> All { get; }

        IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity GetById(Guid id);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<int> SaveAsync();
    }
}
