using CQRS.Application.Categories.Commands;

using MediatR;

namespace CQRS.HostedServices;

public class SyncHostedService(IMediator mediator) : IHostedService, IDisposable
{
    private Timer? _timer;

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(SyncEntities, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(1));

        return Task.CompletedTask;
    }

    private async void SyncEntities(object? state)
    {
        await mediator.Send(new SyncCategoriesCommand());
        await mediator.Send(new SyncProductsCommand());
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