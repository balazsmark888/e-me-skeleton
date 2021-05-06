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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApplicationContext _applicationContext;

        public UserService(IHttpClientFactory httpClientFactory, ApplicationContext applicationContext)
        {
            _httpClientFactory = httpClientFactory;
            _applicationContext = applicationContext;
        }

        public async Task<HttpResponseMessage> RegisterAsync(UserRegistrationDto registrationDto)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var dict = registrationDto.MapToDictionary();
            var content = new FormUrlEncodedContent(dict.Select(p => new KeyValuePair<string, string>(p.Key, p.Value)));
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.RegisterAddress}");
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = content
            };
            request.Headers.Add("accept", "application/json");
            var response = await httpClient.SendAsync(request);
            return response;
        }

        public async Task<UserDto> LoginAsync(AuthDto authDto)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var dict = authDto.MapToDictionary();
            var content = new FormUrlEncodedContent(dict.Select(p => new KeyValuePair<string, string>(p.Key, p.Value)));
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.LoginAddress}");
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = content
            };
            request.Headers.Add("accept", "application/json");
            var responseMessage = await httpClient.SendAsync(request);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var response = await responseMessage.Content.ReadAsStringAsync();
                throw new ApplicationException(response);
            }

            var userDto = JsonConvert.DeserializeObject<UserDto>(await responseMessage.Content.ReadAsStringAsync());
            return userDto;
        }

        public async Task<HttpResponseMessage> LogoutAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.LogoutAddress}");
            var response = await httpClient.PostAsync(uri, new StringContent(string.Empty));

            return response;
        }

        public async Task<UserDetailDto> GetUserDetailAsync(Guid userId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.UserDetailGetAddress}");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = httpClient.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(result);
            }

            var userDto = JsonConvert.DeserializeObject<UserDetailDto>(await response.Content.ReadAsStringAsync());
            return userDto;
        }

        public async Task<bool> UpdateUserDetailAsync(UserDetailDto userDetailDto)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var dict = userDetailDto.MapToDictionary();
            var content = new FormUrlEncodedContent(dict.Select(p => new KeyValuePair<string, string>(p.Key, p.Value)));
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.UserDetailUpdateAddress}");
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            var request = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = content
            };
            request.Headers.Add("accept", "application/json");
            var response = httpClient.SendAsync(request).Result;
            if (response.IsSuccessStatusCode) return true;
            var result = await response.Content.ReadAsStringAsync();
            throw new ApplicationException(result);

        }

        public bool IsAuthenticated()
        {
            var httpClient = _httpClientFactory.CreateClient();
            if (_applicationContext.ApplicationSecureStorage.ContainsKey(Constants.AuthTokenProperty))
            {
                var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.ValidateAddress}");
                var request = new HttpRequestMessage(HttpMethod.Get, uri);
                var response = httpClient.SendAsync(request).Result;
                return response.IsSuccessStatusCode;
            }

            return false;
        }
    }
}
