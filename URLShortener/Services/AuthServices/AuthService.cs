using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using URLShortener.Dtos.AuthDtos;
using URLShortener.Dtos.UserDtos;
using URLShortener.Helpers.Constants.Errors;
using URLShortener.Helpers.ErrorsAndResults;
using URLShortener.Interfaces.Repositories;
using URLShortener.Interfaces.Services;
using URLShortener.Models;

namespace URLShortener.Services.AuthServices;

public class AuthService(
    IRefreshTokenRepository refreshTokenRepository,
    UserManager<User> userManager,
    IGenerateTokenService generateTokenService,
    IMapper mapper
    ) : IAuthService
{
    public async Task<Result<User>> RegisterAsync(CreateUserAppDto registerDto)
    {
        var user = mapper.Map<User>(registerDto);
        var identityResult = await userManager.CreateAsync(user, registerDto.Password);
        if (user.Id == null || !identityResult.Succeeded)
            return Result<User>.Failure(CommonErrors.InternalServerError());
        await userManager.AddToRoleAsync(user, "User");
        
        return Result<User>.Success(user);
    }

    public async Task<Result<AuthTokensResponse>> LoginAsync(LoginDto userDto)
    {
        var user = await userManager.FindByEmailAsync(userDto.Email);
        var isValid = user != null && await userManager.CheckPasswordAsync(user, userDto.Password);
        if (!isValid)
            return Result<AuthTokensResponse>.Failure(CommonErrors.WrongCredentials());
        
        if (!user.IsActive)
            return Result<AuthTokensResponse>.Failure(AuthErrors.UserNotActive);
        
        var response = await GenerateTokenResponse(user);
        if (!response.IsSuccess)
            return Result<AuthTokensResponse>.Failure(response.Error);
        
        await refreshTokenRepository.CreateAsync(new RefreshToken
        {
            Token = response.Value.RefreshToken,
            UserId = user.Id,
            ExpiryDate = response.Value.RefreshExpiresAt
        });
        
        var result = await refreshTokenRepository.SaveChangesAsync();
        return result <= 0 ? Result<AuthTokensResponse>.Failure(CommonErrors.InternalServerError()) : response;
    }

    public async Task<Result<AuthTokensResponse>> RefreshTokenAsync(string accessToken, string refreshToken)
    {
        var principal = await generateTokenService.GetPrincipalFromExpiredToken(accessToken);
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            await refreshTokenRepository.RevokeToken(refreshToken);
            return Result<AuthTokensResponse>.Failure(CommonErrors.InvalidRefreshToken());
        }
        
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return Result<AuthTokensResponse>.Failure(CommonErrors.InvalidRefreshToken());
            
        var token = await refreshTokenRepository.CheckTokenExistsByUserId(refreshToken, userId);
        if (token == null || token.ExpiryDate < DateTime.UtcNow || token.IsRevoked)
            return Result<AuthTokensResponse>.Failure(CommonErrors.InvalidRefreshToken());
        
        var response = await GenerateTokenResponse(user);
        if (!response.IsSuccess)
            return Result<AuthTokensResponse>.Failure(response.Error);
        
        refreshTokenRepository.ReplaceToken(token, response.Value.RefreshToken, response.Value.RefreshExpiresAt);
        await refreshTokenRepository.SaveChangesAsync();

        return response;
    }

    private async Task<Result<AuthTokensResponse>> GenerateTokenResponse(User user)
    {
        var generatToken = generateTokenService.GenerateAccessTokenAsync(user);
        var generateRefreshToken = generateTokenService.GenerateRefreshToken();

        if (!generatToken.IsSuccess)
            return Result<AuthTokensResponse>.Failure(generatToken.Error);
        
        return Result<AuthTokensResponse>.Success(new AuthTokensResponse(
            generatToken.Value,
            generateRefreshToken.Value.Item1,
            generateRefreshToken.Value.Item2));
    }

    public async Task<Result> LogoutAsync(string refreshToken)
    {
        var tokenExists = await refreshTokenRepository.CheckTokenExists(refreshToken);
        if (!tokenExists)
            return Result.Failure(CommonErrors.InvalidRefreshToken());
        
        await refreshTokenRepository.RevokeToken(refreshToken);
        await refreshTokenRepository.SaveChangesAsync();

        return Result.Success();
    }
}
