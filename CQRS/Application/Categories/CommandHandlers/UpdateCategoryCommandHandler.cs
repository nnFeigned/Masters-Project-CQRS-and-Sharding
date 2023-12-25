using CQRS.Application.Categories.Commands;
using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;
using MediatR;

namespace CQRS.Application.Categories.CommandHandlers;

public class UpdateCategoryCommandHandler(IWriteRepository<Category> categoryRepository) : IRequestHandler<UpdateCategoryCommand>
{
    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,

            // It will not work like that, you need to check what was added, what changed, what removed
            // Check https://learn.microsoft.com/en-us/ef/core/saving/disconnected-entities
            // Handling deletes code example
            Products = request.Products
        };

        await categoryRepository.UpdateEntityAsync(category);
    }
}