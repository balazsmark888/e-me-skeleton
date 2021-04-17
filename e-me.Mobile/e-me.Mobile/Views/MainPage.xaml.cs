using System;
using e_me.Mobile.Services.Navigation;
using e_me.Mobile.ViewModels;
using Xamarin.Forms;

namespace e_me.Mobile.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly INavigationService _navigationService;

        public MainPage(IMainViewModel viewModel, INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeComponent();
            BindingContext = viewModel;
        }

        private void LoginButton_OnClicked(object sender, EventArgs e)
        {
            //TODO: change this
            _navigationService.NavigateTo<MainPage>();
        }

        private void RegisterButton_OnClicked(object sender, EventArgs e)
        {
            _navigationService.NavigateTo<RegisterPage>();
        }
    }
}
