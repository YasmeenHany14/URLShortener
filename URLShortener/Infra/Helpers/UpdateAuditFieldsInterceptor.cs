using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using URLShortener.Models.Common;

namespace URLShortener.Infra.Helpers;
public sealed class UpdateAuditFieldsInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;
        if (dbContext is null)
        {
            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);
        }
        
        IEnumerable<EntityEntry<PrimaryKeyBaseEntity>> entries = dbContext
            .ChangeTracker
            .Entries<PrimaryKeyBaseEntity>();
        
        foreach (var entityEntry in entries)
        {
            SetAuditFields(entityEntry);
        }
        return base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);
    }

    void SetAuditFields(EntityEntry<PrimaryKeyBaseEntity> entityEntry)
    {
        if (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified)
            entityEntry.Property(e => e.CreatedAt).CurrentValue = DateTime.Now;
    }
}
