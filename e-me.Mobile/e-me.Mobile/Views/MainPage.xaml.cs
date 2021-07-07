using System;
using e_me.Mobile.Services.Navigation;
using e_me.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_me.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly INavigationService _navigationService;

        public MainPage(MainViewModel viewModel, INavigationService navigationService)
        {
            _navigationService = navigationService;
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

        protected override void OnAppearing()
        {
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}
