using System.Threading.Tasks;
using e_me.Business.DTOs;
using e_me.Business.Interfaces;
using e_me.Mvc.Auth.Interfaces;
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

        public AuthController(IAuthService authService,
            IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }
        /// <summary>
        /// This POST method allows users to authenticate and obtain their JWT token to access protected endpoints.
        /// </summary>
        /// <param name="authDto">DTO containing login information</param>
        /// <returns>Auth information with JWT token</returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] AuthDto authDto)
        {
            try
            {
                var response = await _authService.AuthenticateAsync(authDto.LoginName, authDto.Password);
                if (response == null)
                {
                    return BadRequest(new { message = "LoginName or password is incorrect!" });
                }

                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] UserRegistrationDto userRegistrationDto)
        {
            try
            {
                var result = await _userService.CreateUserAsync(userRegistrationDto);
                //var (fullName, email, token) = await _authService.GenerateResetPasswordAsync(result.Email);
                //emailService.SendWelcomeEmail(Request.Headers["Origin"], email, fullName, token);
                //return CreatedAtRoute("", new { id = result.UserId }, result);
                var response = await _authService.AuthenticateAsync(result.LoginName, userRegistrationDto.Password);
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Validate")]
        public IActionResult Validate()
        {
            return Ok();
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.DeAuthenticateAsync();

            return Ok();
        }
    }
}
