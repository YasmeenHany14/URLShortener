using System.Security.Claims;
using URLShortener.Helpers.ErrorsAndResults;
using URLShortener.Models;

namespace URLShortener.Interfaces.Services;

public interface IGenerateTokenService
{
    Result<string> GenerateAccessTokenAsync(User user);
    Result<(string, DateTime)> GenerateRefreshToken();
    Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
}