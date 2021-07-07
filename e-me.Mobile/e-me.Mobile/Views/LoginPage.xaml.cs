using System;
using AutoMapper;
using e_me.Mobile.AppContext;
using e_me.Mobile.Services.Crypto;
using e_me.Mobile.Services.Navigation;
using e_me.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_me.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly IMapper _mapper;
        private readonly INavigationService _navigationService;
        private readonly LoginViewModel _loginViewModel;
        private readonly ApplicationContext _applicationContext;
        private readonly ICryptoService _cryptoService;

        public LoginPage()
        {

        }

        public LoginPage( IMapper mapper, INavigationService navigationService,
            LoginViewModel loginViewModel, ApplicationContext applicationContext,
            ICryptoService cryptoService)
        {
            _mapper = mapper;
            _navigationService = navigationService;
            _loginViewModel = loginViewModel;
            _applicationContext = applicationContext;
            _cryptoService = cryptoService;
            InitializeComponent();
            BindingContext = loginViewModel;
        }

        private async void LoginButton_OnClicked(object sender, EventArgs e)
        {
            
        }

        protected override void OnAppearing()
        {
            Shell.SetNavBarIsVisible(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
            _navigationService.NavigateTo<MainPage>();
            return true;
        }
    }
}
