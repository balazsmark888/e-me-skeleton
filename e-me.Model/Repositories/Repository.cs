using System;
using System.Linq;
using System.Linq.Expressions;
using e_me.Model.Model;
using e_me.Model.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_me.Model.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly Microsoft.EntityFrameworkCore.DbContext _context;

        protected Repository(Microsoft.EntityFrameworkCore.DbContext context)
        {
            _context = context;
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(_context.Set<TEntity>(), (current, includeProperty) => current.Include(includeProperty));
        }

        public void InsertOrUpdate(TEntity entity)
        {
            if (entity.Id == default)
            {
                _context.Set<TEntity>().Add(entity);
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            _context.Set<TEntity>().Remove(_context.Set<TEntity>().Find(id));
        }

        public void Save()
        {
            using var transaction = _context.Database.BeginTransaction();
            _context.SaveChanges();
            transaction.Commit();
        }
    }
}
