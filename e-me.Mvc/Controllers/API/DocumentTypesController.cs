using System;
using System.Linq;
using AutoMapper;
using e_me.Model.Repositories;
using e_me.Shared.DTOs.Document;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace e_me.Mvc.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypesController : Controller
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IMapper _mapper;

        public DocumentTypesController(IDocumentTypeRepository documentTypeRepository, IMapper mapper)
        {
            _documentTypeRepository = documentTypeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets every DocumentType.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var documentTypes = _documentTypeRepository.All.ToList().Select(_mapper.Map<DocumentTypeDto>);
                return Ok(documentTypes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
