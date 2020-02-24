using Microsoft.AspNet.SignalR.Client;
using Xamarin.Forms;

namespace e_me.mobile
{
    public partial class App
    {
        public HubConnection HubConnection { get; set; }

        public Button Button { get; set; }

        public Label Label { get; set; }


        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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
