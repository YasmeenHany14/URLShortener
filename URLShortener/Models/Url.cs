using URLShortener.Models.Common;

namespace URLShortener.Models;

public class Url : PrimaryKeyBaseEntity
{
    public string OriginalUrl { get; set; } = string.Empty;
    public string ShortUrl { get; set; } = string.Empty; //TODO: check why i made this prop???????//
    public string Code { get; set;  } = string.Empty;
}
