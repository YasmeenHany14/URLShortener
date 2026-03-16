using URLShortener.Common;

namespace URLShortener.Models.Dtos;

public class UrlDto
{
    public string shortUrlCode { get; set; } = string.Empty;
    public UrlDto(string code)
    {
        shortUrlCode = code;
    }
}
