using System;
using System.Collections.ObjectModel;
using e_me.Mobile.Services.DataStores;
using e_me.Mobile.Services.Document;
using e_me.Shared.DTOs.Document;

namespace e_me.Mobile.ViewModels
{
    public class DocumentsViewModel
    {
        private readonly DocumentTemplateListItemDataStore _dataStore;
        private readonly IDocumentService _documentService;

        public ObservableCollection<DocumentTemplateListItemDto> OwnedDocumentTypes =>
            new ObservableCollection<DocumentTemplateListItemDto>(_dataStore.GetOwnedItems());

        public DocumentsViewModel(DocumentTemplateListItemDataStore dataStore, IDocumentService documentService)
        {
            _dataStore = dataStore;
            _documentService = documentService;
        }

        public UserDocumentDto OnItemSelected(DocumentTemplateListItemDto item)
        {
            return item == null ? null : _documentService.GetDocument(item.DocumentTemplateId);
        }

        public void DeleteDocument(Guid templateId)
        {
            _documentService.DeleteDocument(templateId);
        }

        public string GetAccessToken(Guid templateId)
        {
            return _documentService.GetAccessToken(templateId);
        }

        public UserDocumentDto GetDocumentFromCode(string token)
        {
            return _documentService.GetDocumentFromCode(token);
        }
    }
}