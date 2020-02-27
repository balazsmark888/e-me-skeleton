using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using e_me.Model.Models;
using e_me.server.Mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace e_me.server.Mvc.Controllers.API
{

    public class ApiController : Controller
    {
        private IConfiguration Configuration { get; }
        private SignInManager<ApplicationUser> SignInManager { get; }


        public ApiController(IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            Configuration = configuration;
            SignInManager = signInManager;
        }

        /// <summary>
        /// Handles the API requests related to remote login
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Login([FromForm]SimpleLoginModel simpleLoginModel)
        {
            var result = await SignInManager.PasswordSignInAsync(simpleLoginModel.Email, simpleLoginModel.Password, true, false);
            if (result.Succeeded)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.Email, simpleLoginModel.Email),
                };

                var credentials =
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                        SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(Configuration["Jwt:Issuer"],
                    Configuration["Jwt:Audience"], claims, signingCredentials: credentials,
                    expires: DateTime.Now.AddMonths(1));

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return Unauthorized();
        }
    }
}
