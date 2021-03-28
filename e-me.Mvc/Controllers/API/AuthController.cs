using System.Threading.Tasks;
using e_me.Business.DTOs;
using e_me.Business.Interfaces;
using e_me.Mvc.Application;
using e_me.Mvc.Auth.Interfaces;
using e_me.Mvc.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_me.Mvc.Controllers.API
{
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

        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetEmail passwordResetEmail)
        {
            //var (fullName, email, token) = await authService.GenerateResetPasswordAsync(passwordResetEmail.Email);
            //await _emailService.SendResetPasswordEmailAsync(Request.Headers["Origin"], email, fullName, token);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("CheckResetPasswordToken/{token}")]
        public async Task<IActionResult> CheckTokenValidation([FromRoute] string token)
        {
            if (token == null)
            {
                return BadRequest("Reset Password Token not valid!");
            }

            var result = await _authService.IsValidCurrentResetPasswordTokenAsync(token);
            if (result == false)
            {
                return BadRequest("Invalid token!");
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("ConfirmResetPassword/{token}")]
        public async Task<IActionResult> ConfirmResetPassword([FromRoute] string token, [FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (token == null)
            {
                return BadRequest("Reset Password Token not valid!");
            }
            if (!ModelState.IsValid || resetPasswordDto.ConfirmPassword != resetPasswordDto.Password)
            {
                return BadRequest("Password validation not good!");
            }

            var result = await _authService.IsValidCurrentResetPasswordTokenAsync(token);
            if (result == false)
            {
                return BadRequest("Invalid token!");
            }

            result = await _authService.ResetUserPassword(token, resetPasswordDto.Password);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Cannot change password!");
        }
    }
}
