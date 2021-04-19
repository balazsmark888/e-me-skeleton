using System;
using e_me.Mobile.MenuItems;
using e_me.Mobile.Services.Navigation;
using e_me.Mobile.Views;
using Xamarin.Forms;

namespace e_me.Mobile
{
    public partial class AppShell : Shell
    {
        private readonly INavigationService _navigationService;

        public AppShell(INavigationService navigationService,
            DocumentsFlyoutItem documentsFlyoutItem,
            MainFlyoutItem mainFlyoutItem,
            LoginFlyoutItem loginFlyoutItem,
            RegisterFlyoutItem registerFlyoutItem,
            DocumentTypesFlyoutItem documentTypesFlyoutItem)
        {
            _navigationService = navigationService;
            InitializeComponent();
            Items.Add(mainFlyoutItem);
            Items.Add(loginFlyoutItem);
            Items.Add(registerFlyoutItem);
            Items.Add(documentsFlyoutItem);
            Items.Add(documentTypesFlyoutItem);
        }

        private void OnMenuItemClicked(object sender, EventArgs e)
        {
            _navigationService.NavigateTo<MainPage>();
        }

        private void LogoutClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
