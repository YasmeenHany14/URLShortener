using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using URLShortener.Models;

namespace URLShortener.Infra.Configuration;

public class UrlConfiguration : IEntityTypeConfiguration<Url>
{
    public void Configure(EntityTypeBuilder<Url> builder)
    {
        builder.Property(u => u.Id)
            .ValueGeneratedNever();
        builder.Property(u => u.OriginalUrl)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(u => u.EncodedUrl)
            .IsRequired() // add min len later after deciding on the service name
            .HasMaxLength(100); // should be changed later
        builder.Property(u => u.OriginalUrl).IsRequired();
    }
}
