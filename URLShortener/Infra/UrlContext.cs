using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using URLShortener.Models;

namespace URLShortener.Infra;

public class UrlContext : IdentityDbContext<User>
{
    public UrlContext(DbContextOptions<UrlContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasSequence<int>("UrlNumber")
            .StartsAt(100)
            .IncrementsBy(10);
        builder.ApplyConfigurationsFromAssembly(typeof(UrlContext).Assembly);
    }

    private void FilterDeletedEntities(ModelBuilder modelBuilder)
    {
        
    }
    
    // define tables here
    public DbSet<Url> Urls { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
}
