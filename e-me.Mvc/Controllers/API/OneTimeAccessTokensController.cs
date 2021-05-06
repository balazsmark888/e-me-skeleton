using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using e_me.Business.Services.Interfaces;
using e_me.Model.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace e_me.Mvc.Controllers.API
{
    /// <summary>
    /// Controller to handle one-time access tokens.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OneTimeAccessTokensController : Controller
    {
        private readonly IOneTimeAccessTokenRepository _oneTimeAccessTokenRepository;
        private readonly IMapper _mapper;
        private readonly ITokenGeneratorService _tokenGeneratorService;
        private readonly IUserDocumentRepository _userDocumentRepository;
        private readonly IAuthService _authService;
        private readonly IDocumentService _documentService;

        /// <inheritdoc />
        public OneTimeAccessTokensController(IOneTimeAccessTokenRepository oneTimeAccessTokenRepository,
            IMapper mapper,
            ITokenGeneratorService tokenGeneratorService,
            IUserDocumentRepository userDocumentRepository,
            IAuthService authService,
            IDocumentService documentService)
        {
            _oneTimeAccessTokenRepository = oneTimeAccessTokenRepository;
            _mapper = mapper;
            _tokenGeneratorService = tokenGeneratorService;
            _userDocumentRepository = userDocumentRepository;
            _authService = authService;
            _documentService = documentService;
        }

        /// <summary>
        /// Requests a new one-time access token for a document.
        /// </summary>
        /// <param name="templateId">The ID of the template.</param>
        /// <returns>The new token.</returns>
        [HttpGet("requestToken")]
        public async Task<IActionResult> RequestToken([FromForm] Guid templateId)
        {
            try
            {
                var user = await _authService.GetAuthenticatedUserAsync();
                var document = await _userDocumentRepository.GetByUserIdAndTemplateId(user.Id, templateId);
                var userDocumentId = document.Id;
                var newToken = await _documentService.RequestAccessTokenAsync(userDocumentId);
                if (newToken == null) return BadRequest("Access token for this document already exists!");
                return Ok(newToken.Token);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
