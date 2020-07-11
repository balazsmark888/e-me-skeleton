using System;
using System.Linq;
using System.Linq.Expressions;
using e_me.Model.Model;
using e_me.Model.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_me.Model.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly Microsoft.EntityFrameworkCore.DbContext Context;

        protected Repository(Microsoft.EntityFrameworkCore.DbContext context)
        {
            Context = context;
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(Context.Set<TEntity>(), (current, includeProperty) => current.Include(includeProperty));
        }

        public void InsertOrUpdate(TEntity entity)
        {
            if (entity.Id == default)
            {
                Context.Set<TEntity>().Add(entity);
            }
            else
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            Context.Set<TEntity>().Remove(Context.Set<TEntity>().Find(id));
        }

        public void Save()
        {
            using var transaction = Context.Database.BeginTransaction();
            Context.SaveChanges();
            transaction.Commit();
        }
    }
}
