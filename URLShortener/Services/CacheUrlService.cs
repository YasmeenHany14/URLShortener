using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace URLShortener.Services;

public class CacheUrlService(IMemoryCache cache) : ICacheUrlService
{
    public void CacheUrl(string originalUrl, string code)
    {
        var cacheKey = code;
        if (!cache.TryGetValue(cacheKey, out _))
        {
            var entry = originalUrl;
            var timeInMinutes = 1;
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(timeInMinutes));
            cache.Set(cacheKey, entry, cacheEntryOptions);
        }
    }

    public string? GetOriginalUrl(string code)
    {
        if(cache.TryGetValue(code, out string? entry))
            return  entry;
        return null;
    }
}
