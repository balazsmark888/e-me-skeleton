using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using e_me.Mobile.Models;
using e_me.Mobile.Services.DataStores;
using Xamarin.Forms;

namespace e_me.Mobile.ViewModels
{
    public class DocumentTypesViewModel
    {
        private readonly DocumentTypeDataStore _dataStore;

        public ObservableCollection<DocumentType> DocumentTypes { get; }

        public Command ItemTapped { get; }

        public DocumentTypesViewModel(DocumentTypeDataStore dataStore)
        {
            _dataStore = dataStore;
            DocumentTypes = new ObservableCollection<DocumentType>(new List<DocumentType>{new DocumentType{DisplayName = "asdasd", Id = Guid.NewGuid(), Name = "asdasd"}});
            ItemTapped = new Command<DocumentType>(OnItemSelected);
        }

        private void OnItemSelected(DocumentType item)
        {
            if (item == null)
                return;
        }
    }
}
