namespace URLShortener.Services;

public interface ICacheUrlService
{
    void CacheUrl(string originalUrl, string code);
    string? GetOriginalUrl(string code);
}