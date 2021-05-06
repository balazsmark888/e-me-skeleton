using System;
using e_me.Mobile.ViewModels;
using e_me.Shared.DTOs.User;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_me.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetailPage : ContentPage
    {
        private readonly UserDetailViewModel _userDetailViewModel;

        private UserDetailDto _userDetailDto;

        public UserDetailPage(UserDetailViewModel userDetailViewModel)
        {
            _userDetailViewModel = userDetailViewModel;
            _userDetailDto = _userDetailViewModel.GetUserDetailDto();
            InitializeComponent();
            BindingContext = _userDetailDto;
        }

        protected override void OnAppearing()
        {
            try
            {
                SaveButton.IsEnabled = false;
                Shell.SetTabBarIsVisible(this, true);
                _userDetailDto = _userDetailViewModel.GetUserDetailDto();
                BindingContext = _userDetailDto;
            }
            catch (Exception)
            {
                DisplayAlert("Error", "Something went worng.", "OK");
            }
        }

        private void SaveButton_OnClicked(object sender, EventArgs e)
        {
            var dto = BindingContext as UserDetailDto;
            _userDetailViewModel.UpdateUserDetailDto(dto);
        }

        private void CancelButton_OnClicked(object sender, EventArgs e)
        {
            _userDetailDto = _userDetailViewModel.GetUserDetailDto();
            BindingContext = _userDetailDto;
        }

        private void OnFormChanged(object sender, TextChangedEventArgs e)
        {
            SaveButton.IsEnabled = true;
        }

        private void OnDateSelected(object sender, EventArgs e)
        {
            SaveButton.IsEnabled = true;
        }
    }
}