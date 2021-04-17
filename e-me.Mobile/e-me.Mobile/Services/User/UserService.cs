using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using e_me.Mobile.AppContext;
using e_me.Mobile.Helpers;
using e_me.Shared;
using e_me.Shared.DTOs;
using e_me.Shared.DTOs.User;
using Newtonsoft.Json;

namespace e_me.Mobile.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly HttpClient _httpClient;

        public UserService(IHttpClientFactory httpClientFactory, ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<HttpResponseMessage> RegisterAsync(UserRegistrationDto registrationDto)
        {
            var dict = registrationDto.MapToDictionary();
            var content = new FormUrlEncodedContent(dict.Select(p => new KeyValuePair<string, string>(p.Key, p.Value)));
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.RegisterAddress}");
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = content
            };
            request.Headers.Add("accept", "application/json");
            var response = await _httpClient.SendAsync(request);
            return response;
        }

        public async Task<UserDto> LoginAsync(AuthDto authDto)
        {
            var dict = authDto.MapToDictionary();
            var content = new FormUrlEncodedContent(dict.Select(p => new KeyValuePair<string, string>(p.Key, p.Value)));
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.LoginAddress}");

            var response = await _httpClient.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode) return null;

            var userDto = JsonConvert.DeserializeObject<UserDto>(await response.Content.ReadAsStringAsync());
            return userDto;
        }

        public async Task<HttpResponseMessage> LogoutAsync()
        {
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.LogoutAddress}");
            var response = await _httpClient.PostAsync(uri, new StringContent(string.Empty));

            return response;
        }
    }
}
