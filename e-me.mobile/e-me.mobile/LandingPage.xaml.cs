using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static e_me.mobile.Settings.ApplicationSettingConstants;
using static e_me.mobile.Settings.ApplicationSettings;

namespace e_me.mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
        public LandingPage(bool isLoggedIn)
        {
            InitializeComponent();
            LabelWelcome.Text = isLoggedIn ? $"Welcome, {GetSettingOrDefault<string>(UsernameKey)}!" : "Login failed, please try again!";
            if (!isLoggedIn)
            {
                ButtonRetry.IsEnabled = true;
                ButtonRetry.IsVisible = true;
            }
        }
    }
}