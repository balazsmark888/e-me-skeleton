using System.Net;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
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

        protected override async void OnStart()
        {
            HubConnection = new HubConnection("https://192.168.100.80:5001/testhub");
            var proxy = HubConnection.CreateHubProxy("TestHub");
            Label = (Label)MainPage.FindByName("Label");
            Button = (Button)MainPage.FindByName("Button");

            proxy.On<string>("ReceiveMessageFromServer", message => { Label.Text = message; });

            Button.Clicked += delegate { proxy.Invoke<string>("ReceiveMessageFromClient", "This is my message."); };
            await HubConnection.Start();

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private static bool AuthenticateUser(string user, string password, out Cookie authCookie)
        {
            var request = WebRequest.Create("https://192.168.100.80:5001/Identity/Accounts/Login") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = new CookieContainer();

            var authCredentials = "UserName=" + user + "&Password=" + password;
            var bytes = System.Text.Encoding.UTF8.GetBytes(authCredentials);
            request.ContentLength = bytes.Length;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
            }

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                authCookie = response?.Cookies[".AspNet.SharedCookie"];
            }

            return authCookie != null;
        }
    }
}
