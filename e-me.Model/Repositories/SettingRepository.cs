using e_me.Model.Model;
using e_me.Model.Repositories.Interfaces;

namespace e_me.Model.Repositories
{
    public class SettingRepository : Repository<Setting>, ISettingRepository
    {
        public SettingRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context)
        {
        }
    }
}
