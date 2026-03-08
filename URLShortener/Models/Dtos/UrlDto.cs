using URLShortener.Common;

namespace URLShortener.Models.Dtos;

public class UrlDto
{
    public string shortUrl { get; set; } = string.Empty;
    public UrlDto(string code)
    {
        shortUrl = ShortLinkSettings.BaseUrl + code;
    }
}
