using System.Collections.Generic;
using System.Linq;
using e_me.server.Mvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_me.server.Mvc.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public T Find(string id)
        {
            return Context.Set<T>().Find(id);
        }

        public IEnumerable<T> FindAll()
        {
            return Context.Set<T>().AsEnumerable();
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void RemoveById(string id)
        {
            Context.Set<T>().Remove(Find(id));
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
    }
}
