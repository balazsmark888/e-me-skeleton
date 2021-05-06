using System.Collections.ObjectModel;
using e_me.Mobile.Services.DataStores;
using e_me.Mobile.Services.Document;
using e_me.Shared.DTOs.Document;

namespace e_me.Mobile.ViewModels
{
    public class DocumentTemplatesViewModel
    {
        private readonly DocumentTemplateListItemDataStore _dataStore;
        private readonly IDocumentService _documentService;

        public ObservableCollection<DocumentTemplateListItemDto> AvailableDocumentTypes =>
            new ObservableCollection<DocumentTemplateListItemDto>(_dataStore.GetAvailableItems());

        public DocumentTemplatesViewModel(DocumentTemplateListItemDataStore dataStore,
            IDocumentService documentService)
        {
            _dataStore = dataStore;
            _documentService = documentService;
        }

        public UserDocumentDto OnItemSelected(DocumentTemplateListItemDto item)
        {
            return item == null ? null : _documentService.RequestDocumentFromTemplate(item.DocumentTemplateId);
        }
    }
}
