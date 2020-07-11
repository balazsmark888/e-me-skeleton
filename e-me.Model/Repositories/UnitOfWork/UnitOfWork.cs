using e_me.Model.DbContext;
using e_me.Model.Repositories.Interfaces;

namespace e_me.Model.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext.Database.EnsureCreated();
            _applicationDbContext = applicationDbContext;
            SettingRepository = new SettingRepository(_applicationDbContext);
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }

        public ISettingRepository SettingRepository { get; }

        public void Complete()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
