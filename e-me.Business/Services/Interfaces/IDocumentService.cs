using System.Threading.Tasks;
using AutoMapper;
using e_me.Model.Models;
using e_me.Shared.DTOs.Document;

namespace e_me.Business.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<DocumentTemplate> CreateNewDocumentTemplate(DocumentTemplateDto documentTemplateDto);
        UserDocumentDto GetDtoFromDocument(UserDocument document, UserEcdhKeyInformation keyInformation);
        UserDocument GetDocumentFromTemplate(DocumentTemplate documentTemplate, UserDetail userDetail);
    }
}
