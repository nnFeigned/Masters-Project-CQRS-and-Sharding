using CQRS.Application.Categories.Commands;
using CQRS.Domain.Repository;

using MediatR;

using MongoDB.Bson;

namespace CQRS.Application.Categories.CommandHandlers;

public class UpdateImageCommandHandler : IRequestHandler<UpdateCategoryCommand>
{

    private readonly ICategoryRepository _categoryRepository;

    public UpdateImageCommandHandler(ICategoryRepository CategoryRepository)
    {
        _categoryRepository = CategoryRepository;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {

        var CategoryId = new ObjectId(request.Id);

        await _categoryRepository.UpdateCategoryAsync(CategoryId, request.Name, request.Products);
    }
}