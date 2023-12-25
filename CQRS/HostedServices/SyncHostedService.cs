using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;

using Microsoft.EntityFrameworkCore;

namespace CQRS.HostedServices;

public class SyncHostedService(IServiceProvider services) : IHostedService, IDisposable
{
    private Timer? _timer = null;

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(SyncEntities, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(1));

        return Task.CompletedTask;
    }

    private async void SyncEntities(object? state)
    {
        using var scope = services.CreateScope();

        var eventLogRepository = scope.ServiceProvider.GetRequiredService<IEventLogRepository>();
        var syncCategoryRepository = scope.ServiceProvider.GetRequiredService<ISyncRepository<Category>>();
        var syncProductRepository = scope.ServiceProvider.GetRequiredService<ISyncRepository<Product>>();

        await SyncCategories(eventLogRepository, syncCategoryRepository);
        await SyncProducts(eventLogRepository, syncProductRepository);
    }

    private static async Task SyncProducts(IEventLogRepository eventLogRepository, ISyncRepository<Product> syncProductRepository)
    {
        var productLogs = await eventLogRepository.GetAll()
            .Where(log => log.EntityType == nameof(Product))
            .Where(log => !log.Processed)
            .ToListAsync();

        var logsPerProduct = productLogs.GroupBy(log => log.EntityId);

        foreach (var logPerProduct in logsPerProduct)
        {
            var lastLog = logPerProduct.OrderBy(log => log.Timestamp).Last();
            if (lastLog.ActionType != EntityState.Added.ToString())
            {
                var product = await syncProductRepository.GetEntityByIdAsync(lastLog.EntityId);
                await syncProductRepository.AddEntityAsync(product!);
            }
            else if (lastLog.ActionType == EntityState.Modified.ToString())
            {
                var product = await syncProductRepository.GetEntityByIdAsync(lastLog.EntityId);
                await syncProductRepository.UpdateEntityAsync(product!);
            }
            else if (lastLog.ActionType == EntityState.Deleted.ToString())
            {
                await syncProductRepository.DeleteEntityAsync(lastLog.EntityId);
            }
        }

        foreach (var productLog in productLogs)
        {
            productLog.Processed = true;
            await eventLogRepository.UpdateLog(productLog);
        }
    }

    private static async Task SyncCategories(IEventLogRepository eventLogRepository, ISyncRepository<Category> syncCategoryRepository)
    {
        var categoryLogs = await eventLogRepository.GetAll()
            .Where(log => log.EntityType == nameof(Category))
            .Where(log => !log.Processed)
            .ToListAsync();

        var logsPerCategory = categoryLogs.GroupBy(log => log.EntityId);

        foreach (var logPerCategory in logsPerCategory)
        {
            var lastLog = logPerCategory.OrderBy(log => log.Timestamp).Last();
            if (lastLog.ActionType == EntityState.Added.ToString())
            {
                var category = await syncCategoryRepository.GetEntityByIdAsync(lastLog.EntityId);
                await syncCategoryRepository.AddEntityAsync(category!);
            }
            else if (lastLog.ActionType == EntityState.Modified.ToString())
            {
                var category = await syncCategoryRepository.GetEntityByIdAsync(lastLog.EntityId);
                await syncCategoryRepository.UpdateEntityAsync(category!);
            }
            else if (lastLog.ActionType == EntityState.Deleted.ToString())
            {
                await syncCategoryRepository.DeleteEntityAsync(lastLog.EntityId);
            }
        }

        foreach (var categoryLog in categoryLogs)
        {
            categoryLog.Processed = true;
            await eventLogRepository.UpdateLog(categoryLog);
        }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}