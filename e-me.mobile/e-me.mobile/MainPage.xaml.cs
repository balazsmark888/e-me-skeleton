using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;

namespace e_me.mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ButtonLogin_OnClicked(object sender, EventArgs e)
        {
            var client = new HttpClient();
            var values = new Dictionary<string, string>
            {
                {"Email", "asd@asd.com"},
                {"Password", "Alma222..."}
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://192.168.100.80:45455/api/login", content);

            ButtonLogin.Text = await response.Content.ReadAsStringAsync();
        }
    }
}
