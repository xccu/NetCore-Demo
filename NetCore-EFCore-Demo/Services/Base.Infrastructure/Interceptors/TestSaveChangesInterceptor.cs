using Base.ApplicationCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Infrastructure.Interceptors;

public class TestSaveChangesInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        GetLogger(eventData).LogInformation("SavingChanges");
        return base.SavingChanges(eventData, result);
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        GetLogger(eventData).LogInformation("SavedChanges");
        return base.SavedChanges(eventData, result);
    }

    public override void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        GetLogger(eventData).LogInformation("SaveChangesFailed");
        base.SaveChangesFailed(eventData);
    }

    public override async  ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default(CancellationToken))
    {
        GetLogger(eventData).LogInformation("SavingChangesAsync");
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default(CancellationToken))
    {
        GetLogger(eventData).LogInformation("SavedChangesAsync");
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    public override async Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default(CancellationToken))
    {
        GetLogger(eventData).LogInformation("SaveChangesFailedAsync");
        await base.SaveChangesFailedAsync(eventData, cancellationToken);
    }

    private ILogger GetLogger(DbContextEventData eventData)
    {
        var provider = eventData.Context.GetService<IServiceProvider>();
        var factory = provider.GetService<ILoggerFactory>();
        var logger = factory.CreateLogger(Constants.TestCategoryName);
        return logger;
    }
}
