using Base.ApplicationCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Base.Infrastructure.Interceptors;

//https://blog.csdn.net/WuLex/article/details/124839946
public class LoggerSaveChangesInterceptor : SaveChangesInterceptor
{
    private ILogger logger;

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        logger = GetLogger(eventData);
        HandleChanges(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        logger = GetLogger(eventData);
        HandleChanges(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult ThrowingConcurrencyException(ConcurrencyExceptionEventData eventData,InterceptionResult result)
    {
        return base.ThrowingConcurrencyException(eventData, result);
    }

    private void HandleChanges(DbContext dbContext)
    {
        var entries = dbContext.ChangeTracker.Entries().ToArray();
        foreach (var entityEntry in entries)
        {
            var state = entityEntry.State;
            switch (entityEntry.State) 
            {
                case EntityState.Deleted:   OnDelete(entityEntry); break;
                case EntityState.Modified:  OnModified(entityEntry);break;
                case EntityState.Added:     OnAdded(entityEntry); break;
            }           
        }
    }

    private void OnDelete(EntityEntry entityEntry)
    {

        string jsonString = JsonSerializer.Serialize(entityEntry.Entity);
        logger?.LogInformation($"Delete: \r\n{jsonString}");
    }

    private void OnModified(EntityEntry entityEntry)
    {
        var originalValues = entityEntry.OriginalValues.ToObject();
        var originaljsonString = JsonSerializer.Serialize(originalValues);
        string jsonString = JsonSerializer.Serialize(entityEntry.Entity);
        logger?.LogInformation($"Modified: \r\n{originaljsonString}\r\n to \r\n{jsonString}");
    }

    private void OnAdded(EntityEntry entityEntry)
    {
        string jsonString = JsonSerializer.Serialize(entityEntry.Entity);
        logger?.LogInformation($"Added: \r\n{jsonString}");
    }

    private ILogger GetLogger(DbContextEventData eventData)
    {
        var provider = eventData.Context.GetService<IServiceProvider>();
        var factory = provider.GetService<ILoggerFactory>();
        var logger = factory.CreateLogger(Constants.LoggerCategoryName);
        return logger;
    }
}
