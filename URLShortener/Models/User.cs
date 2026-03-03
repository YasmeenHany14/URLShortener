using Microsoft.AspNetCore.Identity;

namespace URLShortener.Models;

public class User : IdentityUser
{
    public bool IsActive { get; set; } = false;
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}
