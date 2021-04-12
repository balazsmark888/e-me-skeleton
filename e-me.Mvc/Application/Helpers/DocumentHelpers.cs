using AutoMapper;
using e_me.Shared.Communication;
using e_me.Core.Helpers;
using e_me.Model.Models;
using e_me.Shared;
using e_me.Shared.DTOs.Document;

namespace e_me.Mvc.Application.Helpers
{
    public static class DocumentHelpers
    {
        /// <summary>
        /// Converts a UserDocument model to a UserDocumentDto object.
        /// </summary>
        /// <param name="document">UserDocument</param>
        /// <param name="keyInformation">Object containing information about encryption</param>
        /// <param name="mapper">Auto-mapper</param>
        /// <returns>The converted UserDocumentDto object</returns>
        public static UserDocumentDto GetDtoFromDocument(UserDocument document, UserEcdhKeyInformation keyInformation, IMapper mapper)
        {
            var documentDto = mapper.Map<UserDocumentDto>(document);
            var (encryptedMessage, hash) = E2EE.Encrypt(document.File.ToBase64String(),
                keyInformation.AesKey.FromBase64String(),
                keyInformation.IV.FromBase64String(), keyInformation.HmacKey.FromBase64String(),
                keyInformation.DerivedHmacKey.FromBase64String());
            documentDto.File = encryptedMessage.ToBase64String();
            documentDto.Hash = hash.ToBase64String();
            return documentDto;
        }

        public static UserDocument GetDocumentFromTemplate(DocumentTemplate documentTemplate, UserDetail userDetail)
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
