using Microsoft.EntityFrameworkCore;
using URLShortener.Common;
using URLShortener.Models;

namespace URLShortener.Infra;

public class UrlContext : DbContext
{
    public UrlContext(DbContextOptions<UrlContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Url>(builder =>
        {
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(ShortLinkSettings.Length);

            builder.Property(e => e.ShortUrl)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.OriginalUrl)
                .HasMaxLength(500)
                .IsRequired();
            
            builder
                .HasIndex(shortenedUrl => shortenedUrl.Code)
                .IsUnique();
        });
    }
    public DbSet<Url> Urls { get; set; }
}
