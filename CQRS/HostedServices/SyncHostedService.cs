using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;

namespace CQRS.HostedServices;

public class SyncHostedService(
    ISyncRepository<Category> syncCategoryReepository,
    ISyncRepository<Product> syncProductRepository) : IHostedService, IDisposable
{
    private Timer? _timer = null;

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(1));

        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        // todo
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