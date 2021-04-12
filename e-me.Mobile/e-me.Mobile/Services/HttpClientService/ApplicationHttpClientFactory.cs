using System;
using System.Net.Http;
using System.Net.Http.Headers;
using e_me.Mobile.AppContext;
using e_me.Mobile.Helpers;
using Microsoft.Extensions.Configuration;

namespace e_me.Mobile.Services.HttpClientService
{
    public class ApplicationHttpClientFactory : IHttpClientFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _applicationContext;

        public ApplicationHttpClientFactory(IConfiguration configuration, ApplicationContext applicationContext)
        {
            _configuration = configuration;
            _applicationContext = applicationContext;
        }

        public HttpClient CreateClient(string name)
        {
            var client = new HttpClient { BaseAddress = new Uri(_configuration["BackendUrl"]) };
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
