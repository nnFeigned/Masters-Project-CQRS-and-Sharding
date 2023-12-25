using CQRS.Application.Categories.Commands;
using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;
using MediatR;

namespace CQRS.Application.Categories.CommandHandlers;

public class CreateCategoryCommandHandler(IWriteRepository<Category> categoryRepository, IWriteRepository<EventLog> eventLogRepository) : IRequestHandler<CreateCategoryCommand, Category>
{
    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
        };

        await categoryRepository.AddEntityAsync(category);

        return category;
    }
}