using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using e_me.Business.Services.Interfaces;
using e_me.Model.Repositories;
using e_me.Shared.DTOs.Document;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_me.Mvc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentTemplatesController : Controller
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IMapper _mapper;
        private readonly IDocumentTemplateRepository _documentTemplateRepository;
        private readonly IDocumentService _documentService;
        private readonly IAuthService _authService;

        public DocumentTemplatesController(IDocumentTypeRepository documentTypeRepository,
            IMapper mapper,
            IDocumentTemplateRepository documentTemplateRepository,
            IDocumentService documentService,
            IAuthService authService)
        {
            _documentTypeRepository = documentTypeRepository;
            _mapper = mapper;
            _documentTemplateRepository = documentTemplateRepository;
            _documentService = documentService;
            _authService = authService;
        }

        /// <summary>
        /// Gets the DocumentTemplate for the specified type.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getbytype")]
        public async Task<IActionResult> GetByType([FromRoute] Guid documentTypeId)
        {
            try
            {
                var documentTemplate = await _documentTemplateRepository.GetByTypeAsync(documentTypeId);
                return Ok(documentTemplate);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Creates a new record in the database.
        /// </summary>
        /// <param name="documentTemplateDto">The DTO for the document template</param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] DocumentTemplateDto documentTemplateDto)
        {
            try
            {
                if (!ModelState.IsValid) return ValidationProblem(ModelState);
                var newEntity = await _documentService.CreateNewDocumentTemplate(documentTemplateDto);
                return Ok(_mapper.Map<DocumentTemplateDto>(newEntity));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets every record from the database.
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var list = (await _documentTemplateRepository.AllIncluding(p => p.DocumentType).ToListAsync()).Select(p => _mapper.Map<DocumentTemplateListItemDto>(p));
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableList()
        {
            try
            {
                var user = await _authService.GetAuthenticatedUserAsync();
                var list = (await _documentTemplateRepository.GetAvailableAsync(user.Id)).Select(p => _mapper.Map<DocumentTemplateListItemDto>(p));
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
