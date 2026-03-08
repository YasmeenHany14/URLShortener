using Microsoft.EntityFrameworkCore;
using URLShortener.Models;

namespace URLShortener.Infra.Repositories;

public class UrlRepository(UrlContext context) : IUrlRepository
{
    public async Task<bool> CodeUnique(string code)
    {
        return await context.Urls.FirstOrDefaultAsync(u => u.Code == code) == null;
    }

    public async Task CreateAsync(Url url)
    {
        await context.Urls.AddAsync(url);
    }

    public async Task<Url?> FindByShortcodeAsync(string shortcode)
    {
        return await context.Urls.FirstOrDefaultAsync(u => u.Code == shortcode);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}
