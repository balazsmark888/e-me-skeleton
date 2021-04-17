using System.Linq;
using e_me.Core.Application;
using e_me.Model.DBContext;
using e_me.Model.Models;

namespace e_me.Model.Repositories
{
    public class ApplicationSettingRepository : BaseRepository<ApplicationSetting>, IApplicationSettingRepository
    {
        public ApplicationSettingRepository(ApplicationDbContext context, ApplicationUserContext userContext)
            : base(context, userContext)
        {
        }

        public ApplicationSetting GetSettingByElement(string element)
        {
            return All.FirstOrDefault(a => a.Code.Equals(element)) ?? new ApplicationSetting { Code = element, Value = string.Empty };
        }
    }

    public interface IApplicationSettingRepository : IBaseRepository<ApplicationSetting>
    {
        public ApplicationSetting GetSettingByElement(string element);
    }
}
