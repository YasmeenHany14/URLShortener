
using URLShortener.Models;

namespace URLShortener.Infra.Repositories;

public interface IUrlRepository
{
    public Task<bool> CodeUnique(string code);
    public Task CreateAsync(Url url);
    public Task<Url?> FindByShortcodeAsync(string shortcode);
    public Task<int> SaveChangesAsync();
}
