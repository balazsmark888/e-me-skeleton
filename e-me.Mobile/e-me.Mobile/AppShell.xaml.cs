using System;
using e_me.Mobile.MenuItems;
using e_me.Mobile.Services.Navigation;
using Xamarin.Forms;

namespace e_me.Mobile
{
    public partial class AppShell : Shell
    {
        private readonly INavigationService _navigationService;

        public TabBar MainTabBar { get; }

        public AppShell(INavigationService navigationService,
            DocumentsTab documentsTab,
            DocumentTypesTab documentTypesTab)
        {
            _navigationService = navigationService;
            InitializeComponent();
            MainTabBar = new TabBar
            {
                Items =
                {
                    documentsTab,
                    documentTypesTab
                }
            };
            Items.Add(MainTabBar);
            SetTabBarIsVisible(this, false);
        }

        private void LogoutClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
