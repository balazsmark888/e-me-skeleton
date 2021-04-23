using System;

namespace e_me.Business.Services.Interfaces
{
    public interface ITokenGeneratorService
    {
        string Generate(string userName, DateTime validTo, string role);
        string GeneratePasswordResetToken(string username, DateTime validTo);
        string GenerateOneTimeAccessToken(Guid userDocumentId, DateTime validTo);
    }
}
