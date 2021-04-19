using System;
using e_me.Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace e_me.Mobile
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public App()
        {
            InitializeComponent();
            MainPage = ServiceProvider.GetService<AppShell>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
