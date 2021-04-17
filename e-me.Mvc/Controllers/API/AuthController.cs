using System.Threading.Tasks;
using e_me.Business.Services.Interfaces;
using e_me.Shared.DTOs;
using e_me.Shared.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_me.Mvc.Controllers.API
{
    /// <summary>
    /// Controller to handle AUTH, registration and logout
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        private readonly IUserService _userService;

        /// <summary>
        /// Constructor that initializes the controller's services using DI.
        /// </summary>
        /// <param name="authService">Authentication service</param>
        /// <param name="userService">Service for user-related operations</param>
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        /// <summary>
        /// This POST method allows users to authenticate and obtain their JWT token to access protected endpoints.
        /// </summary>
        /// <param name="authDto">DTO containing login information</param>
        /// <returns>Auth information with JWT token</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] AuthDto authDto)
        {
            try
            {
                var response = await _authService.AuthenticateAsync(authDto);
                if (response == null)
                {
                    return BadRequest(new { message = "Login name or password is incorrect!" });
                }

                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userRegistrationDto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] UserRegistrationDto userRegistrationDto)
        {
            try
            {
                var result = await _userService.CreateUserAsync(userRegistrationDto);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Validates whether the user is authenticated or not.
        /// </summary>
        /// <returns>OK status code</returns>
        [Authorize]
        [HttpGet("validate")]
        public IActionResult Validate()
        {
            return Ok();
        }

        /// <summary>
        /// De-authenticates the user.
        /// </summary>
        /// <returns>OK status code</returns>
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.DeAuthenticateAsync();

            return Ok();
        }
    }
}
