using CQRS.Application.Categories.Commands;
using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;
using MediatR;

namespace CQRS.Application.Categories.CommandHandlers;

public class DeleteCategoryCommandHandler(IWriteRepository<Category> categoryRepository, IWriteRepository<EventLog> eventLogRepository) : IRequestHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await categoryRepository.DeleteEntityAsync(request.Id);
    }
}