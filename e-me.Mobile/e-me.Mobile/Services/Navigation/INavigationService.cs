using System.Threading.Tasks;
using Xamarin.Forms;

namespace e_me.Mobile.Services.Navigation
{
    public interface INavigationService
    {
        void NavigateTo<TPageModel>() where TPageModel : Page;

        Task GoBackAsync();
    }
}
