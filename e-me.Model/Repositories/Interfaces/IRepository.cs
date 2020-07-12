using System;
using System.Linq;
using System.Linq.Expressions;
using e_me.Model.Model;

namespace e_me.Model.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        TEntity Get(int id);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        void InsertOrUpdate(TEntity entity);

        void Delete(int id);

        void Save();
    }
}
