using URLShortener.Api.Controllers;
using URLShortener.Models.Dtos;

namespace URLShortener.Services;

public interface IUrlShorteningService
{
    Task<string> GenerateCodeAsync();
    Task<UrlDto> CreateUrlAsync(string originUrl);
    Task<string> GetOriginalUrlAsync(string url);
    Task<string> CreateCustomUrlAsync(string customCode, string originUrl);
}