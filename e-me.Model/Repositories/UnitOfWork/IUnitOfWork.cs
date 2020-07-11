using System;
using e_me.Model.Repositories.Interfaces;

namespace e_me.Model.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public ISettingRepository SettingRepository { get; }

        public void Complete();
    }
}
