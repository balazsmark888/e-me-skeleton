using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_me.server.Mvc.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IItemRepository Items { get; }
        int Complete();
    }
}
