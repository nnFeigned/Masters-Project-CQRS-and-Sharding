using CQRS.Application.Categories.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

using MongoDB.Bson;

namespace CQRS.Application.Categories.CommandHandlers;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{

    private readonly IWriteRepository<Category> _categoryRepository;

    public UpdateCategoryCommandHandler(IWriteRepository<Category> CategoryRepository)
    {
        _categoryRepository = CategoryRepository;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {

        var CategoryId = new ObjectId(request.Id);

        var Categor = new Category()
        {
            Id = CategoryId,
            Name = request.Name,
            Products = request.Products
        };

        await _categoryRepository.UpdateAsync(Categor);
    }
}