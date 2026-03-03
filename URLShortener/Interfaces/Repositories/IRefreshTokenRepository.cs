using URLShortener.Models;

namespace URLShortener.Interfaces.Repositories;

public interface IRefreshTokenRepository : IBaseRepository<RefreshToken> 
{
    
    Task<bool> CheckTokenExists(string token);
    Task<RefreshToken?> CheckTokenExistsByUserId(string token, string userId);
    Task<bool> RevokeToken(string token);
    void ReplaceToken(RefreshToken refreshToken, string newRefreshToken, DateTime newRefreshExpiresAt);
}
