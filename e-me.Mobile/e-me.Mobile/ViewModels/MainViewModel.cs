using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace e_me.Mobile.ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        public MainViewModel()
        {
            Hello = "Hello from IoC";
        }

        public string Hello { get; set; }
    }

    public interface IMainViewModel
    {

    }
}
