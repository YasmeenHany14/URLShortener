namespace URLShortener.Interfaces.Helpers;

public interface IUserContext
{
    public Guid UserId { get; }
    public bool IsAuthenticated { get; }
    public bool IsAdmin { get; }
}
