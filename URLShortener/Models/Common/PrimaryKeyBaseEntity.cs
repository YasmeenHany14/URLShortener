namespace URLShortener.Domain.Models.Common;

public abstract class PrimaryKeyBaseEntity : BaseEntity
{
    public int Id { get; set; }
}
