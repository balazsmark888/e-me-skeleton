using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_me.Mobile.Services.Document;
using e_me.Shared.DTOs.Document;

namespace e_me.Mobile.Services.DataStores
{
    public class DocumentDataStore : IDataStore<UserDocumentDto>
    {
        private readonly IDocumentService _documentService;

        public DocumentDataStore(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public Task<bool> AddItemAsync(UserDocumentDto item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(UserDocumentDto item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDocumentDto> GetItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public UserDocumentDto GetItem(Guid id)
        {
            return _documentService.GetDocument(id);
        }

        public Task<IEnumerable<UserDocumentDto>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDocumentDto> GetItems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDocumentDto> GetLocalItems()
        {
            throw new NotImplementedException();
        }
    }
}
