using System;
using e_me.Mobile.Services.Navigation;
using e_me.Mobile.Services.User;
using e_me.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_me.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;

        public MainPage(MainViewModel viewModel, INavigationService navigationService, IUserService userService)
        {
            _navigationService = navigationService;
            _userService = userService;
            if (_userService.IsAuthenticated())
            {
                _navigationService.NavigateTo<DocumentsPage>();
            }
            InitializeComponent();
            BindingContext = viewModel;
        }

        private void LoginButton_OnClicked(object sender, EventArgs e)
        {
            _navigationService.NavigateTo<LoginPage>();
        }

        private void RegisterButton_OnClicked(object sender, EventArgs e)
        {
            _navigationService.NavigateTo<RegisterPage>();
        }
    }
}
