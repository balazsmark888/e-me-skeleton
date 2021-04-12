using e_me.Mobile.AppContext;

namespace e_me.Mobile.ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        public MainViewModel(ApplicationContext applicationContext)
        {
            Hello = "Say hello to my little app";
        }

        public string Hello { get; set; }
    }

    public interface IMainViewModel
    {

    }
}
