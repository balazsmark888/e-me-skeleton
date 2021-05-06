﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using e_me.Business.Services.Interfaces;
using e_me.Model.Repositories;
using e_me.Shared.DTOs.Document;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_me.Mvc.Controllers.API
{
    /// <summary>
    /// Controller to handle UserDocuments.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserDocumentsController : Controller
    {
        private readonly IUserDocumentRepository _userDocumentRepository;
        private readonly IUserEcdhKeyInformationRepository _ecdhKeyInformationRepository;
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly IDocumentTemplateRepository _documentTemplateRepository;
        private readonly IDocumentService _documentService;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        /// <inheritdoc />
        public UserDocumentsController(IUserDocumentRepository userDocumentRepository,
            IMapper mapper,
            IAuthService authService,
            IUserEcdhKeyInformationRepository ecdhKeyInformationRepository,
            IUserDetailRepository userDetailRepository,
            IDocumentTemplateRepository documentTemplateRepository,
            IDocumentService documentService)
        {
            _userDocumentRepository = userDocumentRepository;
            _mapper = mapper;
            _authService = authService;
            _ecdhKeyInformationRepository = ecdhKeyInformationRepository;
            _userDetailRepository = userDetailRepository;
            _documentTemplateRepository = documentTemplateRepository;
            _documentService = documentService;
        }

        /// <summary>
        /// Gets the UserDocument with the specified Id.
        /// </summary>
        /// <param name="templateId">Id of the document</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        [HttpGet("document")]
        public async Task<IActionResult> Get([FromForm] Guid templateId)
        {
            try
            {
                var user = await _authService.GetAuthenticatedUserAsync();
                var document = await _userDocumentRepository.GetByUserIdAndTemplateId(user.Id, templateId);
                if (user.Id != document.UserId)
                {
                    throw new ApplicationException("You are not permitted to view this document.");
                }

                var keyInformation = await _ecdhKeyInformationRepository.GetByUserIdAsync(user.Id);
                var documentDto = _documentService.GetDtoFromDocument(document, keyInformation);
                return Ok(documentDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets the current User's documents.
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetListItems()
        {
            try
            {
                var user = await _authService.GetAuthenticatedUserAsync();
                var documents = _userDocumentRepository.GetByUserId(user.Id);
                var listItems = documents.AsEnumerable().Select(_mapper.Map<UserDocumentListItemDto>);
                return Ok(listItems);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Creates a new document from the DocumentTemplate specified by the templateId.
        /// </summary>
        /// <param name="templateId">Id of the DocumentTemplate</param>
        /// <returns>The newly created document</returns>
        /// <exception cref="ApplicationException"></exception>
        [HttpGet("requestFromTemplate")]
        public async Task<IActionResult> RequestFromTemplate([FromForm] Guid templateId)
        {
            try
            {
                var user = await _authService.GetAuthenticatedUserAsync();
                var existingDocument = await _userDocumentRepository.GetByUserIdAndTemplateId(user.Id, templateId);
                if (existingDocument == null)
                {
                    var template = await _documentTemplateRepository.GetByIdAsync(templateId);
                    var userDetail = await _userDetailRepository.GetByUserIdAsync(user.Id);
                    var newDocument = _documentService.GetDocumentFromTemplate(template, userDetail);

                    await _userDocumentRepository.InsertAsync(newDocument);
                    await _userDocumentRepository.SaveAsync();
                }
                var dbDocument = await _userDocumentRepository.GetByUserIdAndTemplateId(user.Id, templateId);
                var keyInformation = await _ecdhKeyInformationRepository.GetByUserIdAsync(user.Id);
                var documentDto = _documentService.GetDtoFromDocument(dbDocument, keyInformation);
                return Ok(documentDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets the document specified in the one-time access token.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <returns></returns>
        [HttpGet("requestFromCode")]
        public async Task<IActionResult> RequestFromCode([FromForm] string token)
        {
            try
            {
                var user = await _authService.GetAuthenticatedUserAsync();
                var dbDocument = await _documentService.GetDocumentByCodeAsync(token);
                if (dbDocument == null) return BadRequest("Expired token!");
                var keyInformation = await _ecdhKeyInformationRepository.GetByUserIdAsync(user.Id);
                var documentDto = _documentService.GetDtoFromDocument(dbDocument, keyInformation);
                return Ok(documentDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes the UserDocument of the specified template.
        /// </summary>
        /// <param name="templateId">The ID of the DocumentTemplate</param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromForm] Guid templateId)
        {
            try
            {
                var user = await _authService.GetAuthenticatedUserAsync();
                var document = await _userDocumentRepository.GetByUserIdAndTemplateId(user.Id, templateId);
                if (document == null) return Ok();
                _userDocumentRepository.Delete(document);
                await _userDocumentRepository.SaveAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
