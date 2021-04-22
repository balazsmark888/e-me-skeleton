using System;
using AutoMapper;
using e_me.Mobile.AppContext;
using e_me.Mobile.Helpers;
using e_me.Mobile.Services.Crypto;
using e_me.Mobile.Services.Navigation;
using e_me.Mobile.Services.User;
using e_me.Mobile.ViewModels;
using e_me.Shared;
using e_me.Shared.DTOs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_me.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly INavigationService _navigationService;
        private readonly LoginViewModel _loginViewModel;
        private readonly ApplicationContext _applicationContext;
        private readonly ICryptoService _cryptoService;

        public LoginPage()
        {

        }

        public LoginPage(IUserService userService, IMapper mapper, INavigationService navigationService,
            LoginViewModel loginViewModel, ApplicationContext applicationContext,
            ICryptoService cryptoService)
        {
            _userService = userService;
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
            try
            {
                BusyLayout.IsVisible = true;
                BusyIndicator.IsBusy = true;
                var authDto = _mapper.Map<AuthDto>((LoginViewModel)_loginViewModel);
                authDto.PublicKey = _cryptoService.CreatePublicKey().ToBase64String();
                var userDto = await _userService.LoginAsync(authDto);
                if (userDto == null) throw new ApplicationException();
                _cryptoService.SaveKeyInformation(userDto);
                _applicationContext.ApplicationSecureStorage[Constants.AuthTokenProperty] = userDto.Token;
                _navigationService.NavigateTo<AppShell>();
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "One or more errors occurred.", "OK");
            }
            finally
            {
                BusyLayout.IsVisible = false;
                BusyIndicator.IsBusy = false;
            }
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
