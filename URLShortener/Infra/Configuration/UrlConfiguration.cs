using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using URLShortener.Domain.Models;

namespace URLShortener.Infra.Configuration;

public class UrlConfiguration : IEntityTypeConfiguration<Url>
{
    public void Configure(EntityTypeBuilder<Url> builder)
    {
        builder.Property(u => u.EncodedUrl)
            .IsRequired() // add min len later after deciding on the service name
            .HasMaxLength(100); // should be changed later
        builder.Property(u => u.OriginalUrl).IsRequired();
    }
}
