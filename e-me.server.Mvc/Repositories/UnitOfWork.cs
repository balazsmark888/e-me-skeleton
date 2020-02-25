using e_me.server.Mvc.Data;
using e_me.server.Mvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_me.server.Mvc.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Items = new ItemRepository(_context);
        }

        public IItemRepository Items { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
