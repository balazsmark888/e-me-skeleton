using System;
using AutoMapper;
using e_me.Mobile.Services.Navigation;
using e_me.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_me.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private readonly IMapper _mapper;
        private readonly INavigationService _navigationService;
        private readonly RegisterViewModel _registerViewModel;

        public RegisterPage()
        {
            
        }

        public RegisterPage(RegisterViewModel registerViewModel, IMapper mapper, INavigationService navigationService)
        {
            _mapper = mapper;
            _registerViewModel = registerViewModel;
            _navigationService = navigationService;
            InitializeComponent();
            BindingContext = registerViewModel;
        }

        private async void RegisterButton_OnClicked(object sender, EventArgs e)
        {
            
        }

        protected override bool OnBackButtonPressed()
        {
            _navigationService.NavigateTo<MainPage>();
            return true;
        }

        protected override void OnAppearing()
        {
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}