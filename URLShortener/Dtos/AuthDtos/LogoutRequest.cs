namespace URLShortener.Dtos.AuthDtos;

public record LogoutRequest
{
    public string RefreshToken { get; init; }
}