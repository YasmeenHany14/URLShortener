using URLShortener.Models.Common;

namespace URLShortener.Models;

public class Url : PrimaryKeyBaseEntity
{
    public string OriginalUrl { get; set; } = string.Empty;
    public string EncodedUrl { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}
