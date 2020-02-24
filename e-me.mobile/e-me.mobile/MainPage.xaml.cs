using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using e_me.mobile.Settings;

namespace e_me.mobile
{
    [DesignTimeVisible(false)]
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ButtonLogin_OnClicked(object sender, EventArgs e)
        {
            // This bypasses HTTPS certificate problems
            // TODO: remove after development
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (s, cert, chain, sslPolicyErrors) => true
            };

            var client = new HttpClient(clientHandler);
            var values = new Dictionary<string, string>
            {
                {"Email", EntryEmail.Text},
                {"Password", EntryPassword.Text}
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://10.0.2.2:5000/api/login", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ApplicationSettings.SetSetting(ApplicationSettingConstants.UsernameKey, EntryEmail.Text);
                ApplicationSettings.SetSetting(ApplicationSettingConstants.JwtTokenKey,
                    await response.Content.ReadAsStringAsync());
            }

            await Navigation.PushModalAsync(new LandingPage(response.StatusCode == System.Net.HttpStatusCode.OK));
        }
    }
}
