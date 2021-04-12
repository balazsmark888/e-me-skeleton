using e_me.Mobile.Helpers;
using Microsoft.Extensions.Configuration;

namespace e_me.Mobile.AppContext
{
    public class ApplicationContextFactory : IApplicationContextFactory
    {
        private readonly IConfiguration _configuration;

        public ApplicationContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ApplicationContext CreateApplicationContext()
        {
            return new ApplicationContext
            {
                ApplicationProperties = Xamarin.Forms.Application.Current.Properties,
                BackendBaseAddress = _configuration[Constants.BackendBaseAddressProperty]
            };
        }
    }
}
