using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_me.Mobile.Models;
using e_me.Shared.DTOs.Document;

namespace e_me.Mobile.Services.Document
{
    public interface IDocumentService
    {
        Task<IEnumerable<DocumentType>> GetAllDocumentTypesAsync();
        IEnumerable<DocumentType> GetAllDocumentTypes();

        Task<IEnumerable<DocumentTemplateListItemDto>> GetAllDocumentTemplateListItemsAsync();
        IEnumerable<DocumentTemplateListItemDto> GetAllDocumentTemplateListItems();
        IEnumerable<DocumentTemplateListItemDto> GetAvailableDocumentTemplateListItems();

        Task<UserDocumentDto> RequestDocumentFromTemplateAsync(Guid templateId);
        UserDocumentDto RequestDocumentFromTemplate(Guid templateId);

        Task<IEnumerable<UserDocumentListItemDto>> GetAllUserDocumentListItemsAsync();
        IEnumerable<UserDocumentListItemDto> GetAllUserDocumentListItems();

        UserDocumentDto GetDocument(Guid documentId);
    }
}
