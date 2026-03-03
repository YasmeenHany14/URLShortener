using Microsoft.EntityFrameworkCore;
using URLShortener.Interfaces.Repositories;
using URLShortener.Models;

namespace URLShortener.Infra.Repositories;

public class RefreshTokenRepository
    : BaseRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(UrlContext context) : base(context) { }

    public async Task<bool> CheckTokenExists(string token)
    {
        return await Context.RefreshTokens.AnyAsync(x => x.Token == token);
    }

    public async Task<RefreshToken?> CheckTokenExistsByUserId(string token, string userId)
    {
        return await Context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == token && x.UserId == userId);
    }

    public async Task<bool> RevokeToken(string token)
    {
        var result = await Context.RefreshTokens
            .Where(x => x.Token == token)
            .ExecuteUpdateAsync(x => x.SetProperty(r => r.IsRevoked, true));
        return result > 0;
    }

    public void ReplaceToken(RefreshToken refreshToken, string newRefreshToken, DateTime newRefreshExpiresAt)
    {
        refreshToken.Token = newRefreshToken;
        refreshToken.ExpiryDate = newRefreshExpiresAt;
        refreshToken.IsRevoked = false;

        Context.RefreshTokens.Update(refreshToken);
    }
}
