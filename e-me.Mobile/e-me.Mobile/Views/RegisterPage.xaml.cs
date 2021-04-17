using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using e_me.Mobile.Services.Navigation;
using e_me.Mobile.Services.User;
using e_me.Mobile.ViewModels;
using e_me.Shared.DTOs.User;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_me.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly INavigationService _navigationService;
        private readonly IRegisterViewModel _registerViewModel;

        public RegisterPage(IRegisterViewModel registerViewModel, IUserService userService, IMapper mapper, INavigationService navigationService)
        {
            _userService = userService;
            _mapper = mapper;
            _registerViewModel = registerViewModel;
            _navigationService = navigationService;
            InitializeComponent();
            BindingContext = registerViewModel;
        }

        private async void RegisterButton_OnClicked(object sender, EventArgs e)
        {
            var dto = _mapper.Map<UserRegistrationDto>((RegisterViewModel)_registerViewModel);
            var response = await _userService.RegisterAsync(dto);
            if (response.IsSuccessStatusCode)
            {
                _navigationService.NavigateTo<LoginPage>();
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent);
                if (dict.ContainsKey("errors"))
                {
                    if (dict["errors"] is Dictionary<string, string[]> errors)
                    {
                        await DisplayAlert("Error", $"{errors.First().Value.First()}", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "One or more errors occurred.", "OK");
                }
            }
        }
    }
}