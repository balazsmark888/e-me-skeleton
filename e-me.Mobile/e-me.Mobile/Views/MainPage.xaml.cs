using e_me.Mobile.ViewModels;
using Xamarin.Forms;

namespace e_me.Mobile.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(IMainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
