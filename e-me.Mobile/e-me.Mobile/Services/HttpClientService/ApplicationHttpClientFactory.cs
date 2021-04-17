using System;
using System.Net.Http;
using System.Net.Http.Headers;
using e_me.Mobile.AppContext;
using e_me.Mobile.Helpers;

namespace e_me.Mobile.Services.HttpClientService
{
    public class ApplicationHttpClientFactory : IHttpClientFactory
    {
        private readonly ApplicationContext _applicationContext;

        public ApplicationHttpClientFactory(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public HttpClient CreateClient(string name)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(AppSettingsManager.Settings[Constants.BackendBaseAddressProperty])
            };

            if (_applicationContext.ApplicationProperties.ContainsKey(Constants.AuthTokenProperty))
            {
                //add auth header to client
                var token = _applicationContext.ApplicationProperties[Constants.AuthTokenProperty] as string;
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }
    }
}
