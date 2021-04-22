using System.Threading.Tasks;
using AutoMapper;
using e_me.Business.Services.Interfaces;
using e_me.Core.Helpers;
using e_me.Model.Models;
using e_me.Model.Repositories;
using e_me.Shared;
using e_me.Shared.Communication;
using e_me.Shared.DTOs.Document;

namespace e_me.Business.Services.Implementations
{
    public class DocumentService : IDocumentService
    {
        private readonly IMapper _mapper;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IDocumentTemplateRepository _documentTemplateRepository;

        public DocumentService(IMapper mapper,
            IDocumentTypeRepository documentTypeRepository,
            IDocumentTemplateRepository documentTemplateRepository)
        {
            _mapper = mapper;
            _documentTypeRepository = documentTypeRepository;
            _documentTemplateRepository = documentTemplateRepository;
        }

        public async Task<DocumentTemplate> CreateNewDocumentTemplate(DocumentTemplateDto documentTemplateDto)
        {
            var existingDocumentTemplate = await _documentTemplateRepository.GetByTypeAsync(documentTemplateDto.DocumentTypeId);
            if (existingDocumentTemplate != null) return null;
            var newDocumentTemplate = _mapper.Map<DocumentTemplate>(documentTemplateDto);
            await _documentTemplateRepository.InsertAsync(newDocumentTemplate);
            await _documentTemplateRepository.SaveAsync();
            return newDocumentTemplate;
        }

        public UserDocumentDto GetDtoFromDocument(UserDocument document, UserEcdhKeyInformation keyInformation)
        {
            var documentDto = _mapper.Map<UserDocumentDto>(document);
            var (encryptedMessage, hash) = E2EE.Encrypt(document.File.ToBase64String(),
                keyInformation.SharedKey.FromBase64String(),
                keyInformation.IV.FromBase64String(),
                keyInformation.HmacKey.FromBase64String(),
                keyInformation.DerivedHmacKey.FromBase64String());
            documentDto.File = encryptedMessage.ToBase64String();
            documentDto.Hash = hash.ToBase64String();
            return documentDto;
        }

        public UserDocument GetDocumentFromTemplate(DocumentTemplate documentTemplate, UserDetail userDetail)
        {
            var userData = userDetail.MapToDictionary();
            var radDocumentTemplate = DocumentProcessing.GetFixedDocumentFromBytes(documentTemplate.File);
            DocumentProcessing.FillDocumentFormFieldsByFieldNames(radDocumentTemplate, userData);
            var filledDocument = DocumentProcessing.GetBytesFromFixedDocument(radDocumentTemplate);
            var userDocument = new UserDocument
            {
                DocumentTemplateId = documentTemplate.Id,
                File = filledDocument,
                UserId = userDetail.UserId
            };
            return userDocument;
        }
    }
}
