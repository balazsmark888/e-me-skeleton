using System;
using System.Threading.Tasks;
using AutoMapper;
using e_me.Business.Services.Interfaces;
using e_me.Model.Models;
using Microsoft.AspNetCore.Mvc;
using e_me.Model.Repositories;
using e_me.Mvc.Controllers.ValidationAttributes;
using e_me.Shared;
using e_me.Shared.DTOs.User;
using Microsoft.AspNetCore.Authorization;

namespace e_me.Mvc.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : Controller
    {
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public UserDetailsController(IUserDetailRepository userDetailRepository, IMapper mapper, IAuthService authService)
        {
            _userDetailRepository = userDetailRepository;
            _mapper = mapper;
            _authService = authService;
        }

        /// <summary>
        /// Gets the UserDetail of the specified UserId.
        /// </summary>
        /// <param name="userId">UserIdd of the UserDetail.</param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        [ValidateUserId]
        public async Task<IActionResult> Get([FromRoute] Guid userId)
        {
            try
            {
                var userDetail = await _userDetailRepository.GetByUserIdAsync(userId);
                return Ok(_mapper.Map<UserDetailDto>(userDetail));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates the UserDetail for the specified UserId in the database.
        /// </summary>
        /// <param name="userId">The Id of the User.</param>
        /// <param name="userDetailDto">Object containing information about the UserDetail.</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] UserDetailDto userDetailDto)
        {
            try
            {
                if (!ModelState.IsValid) return ValidationProblem(ModelState);
                var user = await _authService.GetAuthenticatedUserAsync();
                var userDetail = _mapper.Map<UserDetail>(userDetailDto);
                var dbUserDetail = await _userDetailRepository.GetByUserIdAsync(user.Id);
                userDetail.UserId = dbUserDetail.UserId;
                userDetail.Id = dbUserDetail.Id;
                Helpers.CopyNotNullProperties(userDetail, dbUserDetail);
                await _userDetailRepository.InsertOrUpdateAsync(dbUserDetail);
                await _userDetailRepository.SaveAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
