using URLShortener.Interfaces.Helpers;

namespace URLShortener.Infra.Helpers;

public sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid UserId =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        Guid.Empty;

    public bool IsAuthenticated =>
        httpContextAccessor
            .HttpContext?
            .User
            .Identity?
            .IsAuthenticated ??
        false;

    public bool IsAdmin =>
        httpContextAccessor
            .HttpContext?
            .User
            .IsInRole("Admin") ??
        false;
}