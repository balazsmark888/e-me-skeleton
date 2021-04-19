using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using e_me.Mobile.AppContext;
using e_me.Mobile.Helpers;
using e_me.Mobile.Models;
using Newtonsoft.Json;

namespace e_me.Mobile.Services.DataStores
{
    public class DocumentTypeDataStore : IDataStore<DocumentType>
    {
        private readonly ApplicationContext _applicationContext;
        private readonly HttpClient _httpClient;

        public DocumentTypeDataStore(IHttpClientFactory httpClientFactory, ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _httpClient = httpClientFactory.CreateClient();
        }

        public Task<bool> AddItemAsync(DocumentType item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(DocumentType item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DocumentType> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DocumentType>> GetItemsAsync(bool forceRefresh = false)
        {
            var uri = new Uri($"{_applicationContext.BackendBaseAddress}{Constants.DocumentTypeGetAddress}");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("accept", "application/json");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IList<DocumentType>>(await response.Content.ReadAsStringAsync());
                _applicationContext.ApplicationSecureStorage[Constants.DocumentTypesProperty] = result;
                return result;
            }

            throw new ApplicationException();
        }

        public IEnumerable<DocumentType> GetLocalItems()
        {
            throw new NotImplementedException();
        }
    }
}
