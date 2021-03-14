using System;

namespace e_me.Mvc.Auth.Interfaces
{
    public interface ITokenGenerator
    {
        string Generate(string userName, DateTime validTo, string role);
        string GeneratePasswordResetToken(string username, DateTime validTo);
    }
}
