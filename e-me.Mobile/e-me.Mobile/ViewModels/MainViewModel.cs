using e_me.Mobile.AppContext;
using e_me.Mobile.Helpers;

namespace e_me.Mobile.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(ApplicationContext applicationContext)
        {
            Hello = AppSettingsManager.Settings[Constants.GreetingMessageProperty];
        }

        public string Hello { get; set; }
    }
}
