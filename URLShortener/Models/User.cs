using Microsoft.AspNetCore.Identity;

namespace URLShortener.Models;

public class User : IdentityUser
{
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}
