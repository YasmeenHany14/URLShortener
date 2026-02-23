using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using URLShortener.Domain.Models;

namespace URLShortener.Infra;

public class UrlContext : IdentityDbContext<User>
{
    public UrlContext(DbContextOptions<UrlContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(UrlContext).Assembly);
    }

    private void FilterDeletedEntities(ModelBuilder modelBuilder)
    {
        
    }
    
    // define tables here
    DbSet<Url> Urls { get; set; }
    DbSet<RefreshToken> RefreshTokens { get; set; }
}
