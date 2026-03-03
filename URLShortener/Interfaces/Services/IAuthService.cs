using URLShortener.Dtos.AuthDtos;
using URLShortener.Dtos.UserDtos;
using URLShortener.Helpers.ErrorsAndResults;
using URLShortener.Models;

namespace URLShortener.Interfaces.Services;

public interface IAuthService
{
    Task<Result<User>> RegisterAsync(CreateUserAppDto registerDto);
    Task<Result<AuthTokensResponse>> LoginAsync(LoginDto userDto);
    Task<Result<AuthTokensResponse>> RefreshTokenAsync(string accessToken, string refreshToken);
    Task<Result> LogoutAsync(string refreshToken);
}