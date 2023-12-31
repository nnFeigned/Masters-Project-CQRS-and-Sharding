using CQRS.Application.Categories.Commands;
using MediatR;

namespace CQRS.HostedServices
{
    public class SyncHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SyncHostedService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await SyncEntities(mediator);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task SyncEntities(IMediator mediator)
        {
            await mediator.Send(new SyncCategoriesCommand());
            await mediator.Send(new SyncProductsCommand());
        }
    }
}
