using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using e_me.Mobile.AppContext;
using e_me.Mobile.Helpers;
using e_me.Mobile.Models;
using e_me.Mobile.Services.Crypto;
using e_me.Shared;
using e_me.Shared.Communication;
using e_me.Shared.DTOs.Document;
using Newtonsoft.Json;

namespace e_me.Mobile.Services.Document
{
    public class DocumentService : IDocumentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApplicationContext _applicationContext;
        private readonly ICryptoService _cryptoService;

        public DocumentService(IHttpClientFactory httpClientFactory,
            ApplicationContext applicationContext,
            ICryptoService cryptoService)
        {
            _httpClientFactory = httpClientFactory;
            _applicationContext = applicationContext;
            _cryptoService = cryptoService;
        }

        public async Task<IEnumerable<DocumentType>> GetAllDocumentTypesAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.DocumentTypeGetAddress}");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) throw new ApplicationException();
            var result = JsonConvert.DeserializeObject<IList<DocumentType>>(await response.Content.ReadAsStringAsync());
            _applicationContext.ApplicationSecureStorage[Constants.DocumentTypesProperty] = result;
            return result;

        }

        public IEnumerable<DocumentType> GetAllDocumentTypes()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.DocumentTypeGetAddress}");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = httpClient.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode) throw new ApplicationException();
            var result = JsonConvert.DeserializeObject<IList<DocumentType>>(response.Content.ReadAsStringAsync().Result);
            _applicationContext.ApplicationSecureStorage[Constants.DocumentTypesProperty] = result;
            return result;

        }

        public Task<IEnumerable<DocumentTemplateListItemDto>> GetAllDocumentTemplateListItemsAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DocumentTemplateListItemDto> GetAllDocumentTemplateListItems()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.DocumentTemplateListItemGetAddress}");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = httpClient.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode) throw new ApplicationException();
            var result = JsonConvert.DeserializeObject<IList<DocumentTemplateListItemDto>>(response.Content.ReadAsStringAsync().Result);
            _applicationContext.ApplicationSecureStorage[Constants.DocumentTypesProperty] = result;
            return result;
        }

        public IEnumerable<DocumentTemplateListItemDto> GetAvailableDocumentTemplateListItems()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.DocumentTemplateAvailableListItemGetAddress}");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = httpClient.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode) throw new ApplicationException();
            var result = JsonConvert.DeserializeObject<IList<DocumentTemplateListItemDto>>(response.Content.ReadAsStringAsync().Result);
            _applicationContext.ApplicationSecureStorage[Constants.DocumentTypesProperty] = result;
            return result;
        }

        public IEnumerable<DocumentTemplateListItemDto> GetOwnedDocumentTemplateListItems()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.DocumentTemplateOwnedListItemGetAddress}");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = httpClient.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode) throw new ApplicationException();
            var result = JsonConvert.DeserializeObject<IList<DocumentTemplateListItemDto>>(response.Content.ReadAsStringAsync().Result);
            _applicationContext.ApplicationSecureStorage[Constants.DocumentTypesProperty] = result;
            return result;
        }

        public async Task<UserDocumentDto> RequestDocumentFromTemplateAsync(Guid templateId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.UserDocumentRequestFromTemplateAddress}/{templateId}");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = httpClient.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode) throw new ApplicationException();
            var result = JsonConvert.DeserializeObject<UserDocumentDto>(await response.Content.ReadAsStringAsync());
            var keyInfo = _cryptoService.GetKeyInformation();
            if (result != null)
            {
                result.File = E2EE.Decrypt(result.File.FromBase64String(),
                    keyInfo.IV.FromBase64String(),
                    result.Hash.FromBase64String(),
                    keyInfo.SharedKey.FromBase64String(),
                    keyInfo.HmacKey.FromBase64String(),
                    keyInfo.DerivedHmacKey.FromBase64String());
            }
            return result;
        }

        public UserDocumentDto RequestDocumentFromTemplate(Guid templateId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.UserDocumentRequestFromTemplateAddress}");
            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("templateId", templateId.ToString()) });
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            var request = new HttpRequestMessage(HttpMethod.Get, uri)
            {
                Content = content
            };
            var response = httpClient.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode) throw new ApplicationException();
            var result = JsonConvert.DeserializeObject<UserDocumentDto>(response.Content.ReadAsStringAsync().Result);
            var keyInfo = _cryptoService.GetKeyInformation();
            if (result != null)
            {
                result.File = E2EE.Decrypt(result.File.FromBase64String(),
                    keyInfo.IV.FromBase64String(),
                    result.Hash.FromBase64String(),
                    keyInfo.SharedKey.FromBase64String(),
                    keyInfo.HmacKey.FromBase64String(),
                    keyInfo.DerivedHmacKey.FromBase64String());
            }
            return result;
        }

        public async Task<IEnumerable<UserDocumentListItemDto>> GetAllUserDocumentListItemsAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.UserDocumentListItemGetAddress}");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) throw new ApplicationException();
            var result = JsonConvert.DeserializeObject<IList<UserDocumentListItemDto>>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public IEnumerable<UserDocumentListItemDto> GetAllUserDocumentListItems()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.UserDocumentListItemGetAddress}");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = httpClient.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode) throw new ApplicationException();
            var result = JsonConvert.DeserializeObject<IList<UserDocumentListItemDto>>(response.Content.ReadAsStringAsync().Result);
            return result;
        }

        public UserDocumentDto GetDocument(Guid templateId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.UserDocumentGetAddress}?id={templateId}");
            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("templateId", templateId.ToString()) });
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            var request = new HttpRequestMessage(HttpMethod.Get, uri)
            {
                Content = content
            };
            var response = httpClient.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode) throw new ApplicationException();
            var result = JsonConvert.DeserializeObject<UserDocumentDto>(response.Content.ReadAsStringAsync().Result);
            var keyInfo = _cryptoService.GetKeyInformation();
            if (result != null)
            {
                result.File = E2EE.Decrypt(result.File.FromBase64String(),
                    keyInfo.IV.FromBase64String(),
                    result.Hash.FromBase64String(),
                    keyInfo.SharedKey.FromBase64String(),
                    keyInfo.HmacKey.FromBase64String(),
                    keyInfo.DerivedHmacKey.FromBase64String());
            }
            return result;
        }

        public UserDocumentDto GetDocumentFromCode(string token)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.UserDocumentFromCodeGetAddress}");
            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("token", token) });
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            var request = new HttpRequestMessage(HttpMethod.Get, uri)
            {
                Content = content
            };
            var response = httpClient.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode) throw new ApplicationException();
            var result = JsonConvert.DeserializeObject<UserDocumentDto>(response.Content.ReadAsStringAsync().Result);
            var keyInfo = _cryptoService.GetKeyInformation();
            if (result != null)
            {
                result.File = E2EE.Decrypt(result.File.FromBase64String(),
                    keyInfo.IV.FromBase64String(),
                    result.Hash.FromBase64String(),
                    keyInfo.SharedKey.FromBase64String(),
                    keyInfo.HmacKey.FromBase64String(),
                    keyInfo.DerivedHmacKey.FromBase64String());
            }
            return result;
        }

        public void DeleteDocument(Guid templateId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.UserDocumentDeleteAddress}");
            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("templateId", templateId.ToString()) });
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = content
            };
            var _ = httpClient.SendAsync(request).Result;
        }

        public string GetAccessToken(Guid templateId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.AccessTokenGetAddress}");
            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("templateId", templateId.ToString()) });
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            var request = new HttpRequestMessage(HttpMethod.Get, uri)
            {
                Content = content
            };
            var response = httpClient.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode) throw new ApplicationException();
            var result = response.Content.ReadAsStringAsync().Result;
            return result;
        }
    }
}
