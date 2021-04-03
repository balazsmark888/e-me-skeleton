﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using e_me.Business.DTOs.Document;
using e_me.Business.Services.Interfaces;
using e_me.Model.Repositories;
using e_me.Mvc.Application.Helpers;
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
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        /// <inheritdoc />
        public UserDocumentsController(IUserDocumentRepository userDocumentRepository, IMapper mapper, IAuthService authService, IUserEcdhKeyInformationRepository ecdhKeyInformationRepository, IUserDetailRepository userDetailRepository, IDocumentTemplateRepository documentTemplateRepository)
        {
            _userDocumentRepository = userDocumentRepository;
            _mapper = mapper;
            _authService = authService;
            _ecdhKeyInformationRepository = ecdhKeyInformationRepository;
            _userDetailRepository = userDetailRepository;
            _documentTemplateRepository = documentTemplateRepository;
        }

        /// <summary>
        /// Gets the UserDocument with the specified Id.
        /// </summary>
        /// <param name="id">Id of the document</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        [HttpGet("id")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var document = await _userDocumentRepository.GetByIdAsync(id);
                var user = await _authService.GetAuthenticatedUserAsync();
                if (user.Id != document.UserId)
                {
                    throw new ApplicationException("You are not permitted to view this document.");
                }

                var keyInformation = await _ecdhKeyInformationRepository.GetByUserIdAsync(user.Id);
                var documentDto = DocumentHelpers.GetDtoFromDocument(document, keyInformation, _mapper);
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
        public async Task<IActionResult> RequestFromTemplate([FromRoute] Guid templateId)
        {
            try
            {
                var user = await _authService.GetAuthenticatedUserAsync();
                var alreadyHasDocument = _userDocumentRepository.All.Any(p => p.UserId == user.Id && p.DocumentTemplateId == templateId);
                if (alreadyHasDocument) throw new ApplicationException("You already own this type of document.");
                var template = await _documentTemplateRepository.GetByIdAsync(templateId);
                var userDetail = await _userDetailRepository.GetByUserIdAsync(user.Id);
                var newDocument = DocumentHelpers.GetDocumentFromTemplate(template, userDetail);

                await _userDocumentRepository.InsertOrUpdateAsync(newDocument);
                await _userDocumentRepository.SaveAsync();
                var dbDocument = await _userDocumentRepository.GetByUserIdAndTemplateId(user.Id, template.Id);
                var keyInformation = await _ecdhKeyInformationRepository.GetByUserIdAsync(user.Id);
                var documentDto = DocumentHelpers.GetDtoFromDocument(dbDocument, keyInformation, _mapper);
                return Ok(documentDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}