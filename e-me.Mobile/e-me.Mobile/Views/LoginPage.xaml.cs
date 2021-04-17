using System;
using AutoMapper;
using e_me.Mobile.AppContext;
using e_me.Mobile.Helpers;
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
        private readonly ILoginViewModel _loginViewModel;
        private readonly ApplicationContext _applicationContext;

        public LoginPage(IUserService userService, IMapper mapper, INavigationService navigationService, ILoginViewModel loginViewModel, ApplicationContext applicationContext)
        {
            _userService = userService;
            _mapper = mapper;
            _navigationService = navigationService;
            _loginViewModel = loginViewModel;
            _applicationContext = applicationContext;
            InitializeComponent();
            BindingContext = loginViewModel;
        }

        private async void LoginButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var dto = _mapper.Map<AuthDto>((LoginViewModel)_loginViewModel);
                var result = await _userService.LoginAsync(dto);
                if (result != null)
                {
                    _applicationContext.ApplicationProperties[Constants.AuthTokenProperty] = result.Token;
                    _applicationContext.ApplicationProperties[Constants.E2EEIVProperty] = result.IV.ToBase64String();
                    _applicationContext.ApplicationProperties[Constants.ServerPublicKeyProperty] = result.PublicKey.ToBase64String();
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "One or more errors occurred.", "OK");
            }

        }
    }
}
