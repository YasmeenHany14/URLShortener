using URLShortener.Interfaces.Repositories;
using URLShortener.Models;

namespace URLShortener.Infra.Repositories;

public class UrlRepository(UrlContext context) : BaseRepository<Url>(context), IUrlRepository
{
}
