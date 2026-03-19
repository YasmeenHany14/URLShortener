using URLShortener.Infra.Repositories;
using URLShortener.Services;

namespace UrlShortenerTest.Services;

public class UrlShorteningServiceTests
{
    private readonly IUrlRepository _urlRepository;
    private readonly ICacheUrlService _cacheUrlService;
}