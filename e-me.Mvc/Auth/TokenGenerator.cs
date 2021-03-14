using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using e_me.Mvc.Auth.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace e_me.Mvc.Auth
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly AuthSettings _authSettings;

        public TokenGenerator(IOptions<AuthSettings> authSettings)
        {
            _authSettings = authSettings.Value;
        }

        public string Generate(string userName, DateTime validTo, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _authSettings.Issuer,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userName)
                }),
                Claims = new Dictionary<string, object>
                {
                    {ClaimTypes.Role, role }
                },
                Expires = validTo,
                SigningCredentials = CreateSigningCredentials()
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GeneratePasswordResetToken(string username, DateTime validTo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _authSettings.Issuer,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = validTo,
                SigningCredentials = CreateSigningCredentials()
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private SigningCredentials CreateSigningCredentials()
        {
            var key = Encoding.ASCII.GetBytes(_authSettings.SecretKey);
            return new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
