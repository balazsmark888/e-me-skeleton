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

        /// <inheritdoc />
        public OneTimeAccessTokensController(IOneTimeAccessTokenRepository oneTimeAccessTokenRepository,
            IMapper mapper,
            ITokenGeneratorService tokenGeneratorService,
            IUserDocumentRepository userDocumentRepository)
        {
            _oneTimeAccessTokenRepository = oneTimeAccessTokenRepository;
            _mapper = mapper;
            _tokenGeneratorService = tokenGeneratorService;
            _userDocumentRepository = userDocumentRepository;
        }

        /// <summary>
        /// Requests a new one-time access token for a document.
        /// </summary>
        /// <param name="userDocumentId">The ID of the document.</param>
        /// <returns>The new token.</returns>
        [HttpGet("requestToken")]
        public async Task<IActionResult> RequestToken(Guid userDocumentId)
        {
            try
            {
                if (!await _userDocumentRepository.IsValidUserDocumentId(userDocumentId))
                    return BadRequest("Document not found!");
                var newToken = await _oneTimeAccessTokenRepository.RequestAccessTokenAsync(userDocumentId);
                if (newToken == null) return BadRequest("Access token for this document already exists!");
                newToken.Token = _tokenGeneratorService.GenerateOneTimeAccessToken(userDocumentId, newToken.ValidTo);
                await _oneTimeAccessTokenRepository.InsertOrUpdateAsync(newToken);
                await _oneTimeAccessTokenRepository.SaveAsync();
                return Ok(newToken);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
