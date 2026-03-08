namespace URLShortener.Models.Common;

public abstract class PrimaryKeyBaseEntity
{
    public int Id { get; set; }
    public DateTime  CreatedAt { get; set; }
}
