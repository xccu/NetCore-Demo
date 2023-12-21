using Base.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Base.Infrastructure.Interceptors;

public class ConcurrencySaveChangeInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        HandleChanges(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        HandleChanges(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult ThrowingConcurrencyException(ConcurrencyExceptionEventData eventData, InterceptionResult result)
    {
        return base.ThrowingConcurrencyException(eventData, result);
    }

    public override async ValueTask<InterceptionResult> ThrowingConcurrencyExceptionAsync(ConcurrencyExceptionEventData eventData,
                                                                                      InterceptionResult result,
                                                                                      CancellationToken cancellationToken = default)
    {
        return await base.ThrowingConcurrencyExceptionAsync(eventData, result, cancellationToken);
    }

    private void HandleChanges(DbContext dbContext)
    {
        var entries = dbContext.ChangeTracker.Entries().ToArray();
        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added || 
                entityEntry.State == EntityState.Modified ||
                entityEntry.State == EntityState.Deleted)
            {
                UpdateVersionNo(entityEntry);
            }               
        }
    }

    private void UpdateVersionNo(EntityEntry entityEntry)
    {
        if (entityEntry.Entity is IVersion)
        {
            var propertyEntry = entityEntry.Property("VersionNo");
            if (entityEntry.State == EntityState.Modified || entityEntry.State == EntityState.Deleted)
            {
                propertyEntry.OriginalValue = propertyEntry.CurrentValue;
            }
            if (entityEntry.State != EntityState.Deleted)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    if (Convert.ToInt32(propertyEntry.OriginalValue) < 1)
                    {
                        propertyEntry.CurrentValue = 1;
                    }
                }
                else
                {
                    propertyEntry.CurrentValue = Convert.ToInt32(propertyEntry.OriginalValue) + 1;
                }
            }
        }
    }
}
