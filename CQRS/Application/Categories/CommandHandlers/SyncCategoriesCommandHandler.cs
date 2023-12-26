using CQRS.Application.Categories.Commands;
using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Categories.CommandHandlers;

public class SyncCategoriesCommandHandler(IEventLogRepository eventLogRepository, ISyncRepository<Category> syncCategoryRepository) : IRequestHandler<SyncCategoriesCommand>
{
    public async Task Handle(SyncCategoriesCommand request, CancellationToken cancellationToken)
    {
        var categoryLogs = await eventLogRepository.GetAll()
            .Where(log => log.EntityType == nameof(Category))
            .Where(log => !log.Processed)
            .ToListAsync(cancellationToken: cancellationToken);

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
                await syncCategoryRepository.UpsertEntityAsync(category!);
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
}