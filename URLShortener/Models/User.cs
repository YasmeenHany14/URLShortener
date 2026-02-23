using Microsoft.AspNetCore.Identity;

namespace URLShortener.Domain.Models;

public class User : IdentityUser
{
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}
