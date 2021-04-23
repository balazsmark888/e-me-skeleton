using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using e_me.Mobile.AppContext;
using e_me.Mobile.Models;
using e_me.Mobile.Services.Document;

namespace e_me.Mobile.Services.DataStores
{
    public class DocumentTypeDataStore : IDataStore<DocumentType>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApplicationContext _applicationContext;
        private readonly IDocumentService _documentService;

        public DocumentTypeDataStore(IHttpClientFactory httpClientFactory,
            ApplicationContext applicationContext,
            IDocumentService documentService)
        {
            _httpClientFactory = httpClientFactory;
            _applicationContext = applicationContext;
            _documentService = documentService;
        }

        public Task<bool> AddItemAsync(DocumentType item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(DocumentType item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DocumentType> GetItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public DocumentType GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DocumentType>> GetItemsAsync()
        {
            return await _documentService.GetAllDocumentTypesAsync();
        }

        public IEnumerable<DocumentType> GetItems()
        {
            return _documentService.GetAllDocumentTypes();
        }

        public IEnumerable<DocumentType> GetLocalItems()
        {
            throw new NotImplementedException();
        }
    }
}
