using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using e_me.Business.Services.Interfaces;
using e_me.Core.Helpers;
using e_me.Model.Models;
using e_me.Model.Repositories;
using e_me.Shared;
using e_me.Shared.Communication;
using e_me.Shared.DTOs.Document;
using Guid = System.Guid;

namespace e_me.Business.Services.Implementations
{
    public class DocumentService : IDocumentService
    {
        private readonly IMapper _mapper;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IDocumentTemplateRepository _documentTemplateRepository;
        private readonly ITokenGeneratorService _tokenGeneratorService;
        private readonly IOneTimeAccessTokenRepository _oneTimeAccessTokenRepository;
        private readonly IUserDocumentRepository _userDocumentRepository;

        public DocumentService(IMapper mapper,
            IDocumentTypeRepository documentTypeRepository,
            IDocumentTemplateRepository documentTemplateRepository,
            ITokenGeneratorService tokenGeneratorService,
            IOneTimeAccessTokenRepository oneTimeAccessTokenRepository,
            IUserDocumentRepository userDocumentRepository)
        {
            _mapper = mapper;
            _documentTypeRepository = documentTypeRepository;
            _documentTemplateRepository = documentTemplateRepository;
            _tokenGeneratorService = tokenGeneratorService;
            _oneTimeAccessTokenRepository = oneTimeAccessTokenRepository;
            _userDocumentRepository = userDocumentRepository;
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

        public async Task<OneTimeAccessToken> RequestAccessTokenAsync(Guid userDocumentId)
        {
            var existingToken = await _oneTimeAccessTokenRepository.FindByUserDocumentIdAsync(userDocumentId);
            if (existingToken != null)
            {
                existingToken.Token = _tokenGeneratorService.GenerateOneTimeAccessToken(userDocumentId, DateTime.Now.AddHours(1));
                existingToken.ValidTo = DateTime.Now.AddHours(1);
                existingToken.IsValid = true;
                await _oneTimeAccessTokenRepository.InsertOrUpdateAsync(existingToken);
                await _oneTimeAccessTokenRepository.SaveAsync();
                return existingToken;
            }

            var newToken = new OneTimeAccessToken
            {
                Id = Guid.NewGuid(),
                IsValid = true,
                UserDocumentId = userDocumentId,
                ValidTo = DateTime.Now.AddHours(1),
                Token = _tokenGeneratorService.GenerateOneTimeAccessToken(userDocumentId, DateTime.Now.AddHours(1))
            };
            await _oneTimeAccessTokenRepository.InsertAsync(newToken);
            await _oneTimeAccessTokenRepository.SaveAsync();
            return newToken;
        }

        public async Task<UserDocument> GetDocumentByCodeAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var validTo = DateTime.Parse(jwtToken.Claims.First(p => p.Type == ClaimTypes.Expiration).Value);
            if (validTo <= DateTime.Now) return null;
            var documentId = Guid.Parse(jwtToken.Claims.First(p => p.Type == "documentId").Value);
            return await _userDocumentRepository.GetByIdAsync(documentId);
        }
    }
}
