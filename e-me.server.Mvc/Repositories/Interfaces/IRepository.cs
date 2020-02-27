using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_me.server.Mvc.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Find(string id);

        IEnumerable<T> FindAll();
        
        void Add(T entity);

        void RemoveById(string id);

        void Remove(T entity);
    }
}
