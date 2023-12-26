using CQRS.Application.Categories.Commands;
using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Categories.CommandHandlers;

public class SyncProductsCommandHandler(IEventLogRepository eventLogRepository, ISyncRepository<Product> syncProductRepository) : IRequestHandler<SyncProductsCommand>
{
    public async Task Handle(SyncProductsCommand request, CancellationToken cancellationToken)
    {
        var productLogs = await eventLogRepository.GetAll()
            .Where(log => log.EntityType == nameof(Product))
            .Where(log => !log.Processed)
            .ToListAsync(cancellationToken: cancellationToken);

        var logsPerProduct = productLogs.GroupBy(log => log.EntityId);

        foreach (var logPerProduct in logsPerProduct)
        {
            var lastLog = logPerProduct.OrderBy(log => log.Timestamp).Last();
            if (lastLog.ActionType == EntityState.Added.ToString())
            {
                var product = await syncProductRepository.GetEntityByIdAsync(lastLog.EntityId);
                await syncProductRepository.AddEntityAsync(product!);
            }
            else if (lastLog.ActionType == EntityState.Modified.ToString())
            {
                var product = await syncProductRepository.GetEntityByIdAsync(lastLog.EntityId);
                await syncProductRepository.UpsertEntityAsync(product!);
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
}