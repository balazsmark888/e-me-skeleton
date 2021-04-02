using e_me.Core.Application;

namespace e_me.Business.Services.Interfaces
{
    public interface IApplicationUserContextFactory
    {
        ApplicationUserContext Create();
    }
}
