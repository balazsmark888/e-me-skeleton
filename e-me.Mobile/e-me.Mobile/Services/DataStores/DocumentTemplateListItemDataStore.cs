using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_me.Mobile.Services.Document;
using e_me.Shared.DTOs.Document;

namespace e_me.Mobile.Services.DataStores
{
    public class DocumentTemplateListItemDataStore : IDataStore<DocumentTemplateListItemDto>
    {
        private readonly IDocumentService _documentService;

        public DocumentTemplateListItemDataStore(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public Task<bool> AddItemAsync(DocumentTemplateListItemDto item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(DocumentTemplateListItemDto item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DocumentTemplateListItemDto> GetItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public DocumentTemplateListItemDto GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DocumentTemplateListItemDto>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DocumentTemplateListItemDto> GetItems()
        {
            return _documentService.GetAllDocumentTemplateListItems();
        }

        public IEnumerable<DocumentTemplateListItemDto> GetAvailableItems()
        {
            return _documentService.GetAvailableDocumentTemplateListItems();
        }

        public IEnumerable<DocumentTemplateListItemDto> GetOwnedItems()
        {
            return _documentService.GetOwnedDocumentTemplateListItems();
        }

        public IEnumerable<DocumentTemplateListItemDto> GetLocalItems()
        {
            throw new NotImplementedException();
        }
    }
}
