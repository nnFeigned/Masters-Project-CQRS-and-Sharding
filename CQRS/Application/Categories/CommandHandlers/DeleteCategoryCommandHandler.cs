using CQRS.Application.Categories.Commands;
using CQRS.Domain.Repository;

using MediatR;

using MongoDB.Bson;

namespace CQRS.Application.Categories.CommandHandlers;

public class DeleteImageCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    
    private readonly ICategoryRepository _categoryRepository;


    public DeleteImageCommandHandler(ICategoryRepository CategoryRepository)
    {
        _categoryRepository = CategoryRepository;
    }

    Task IRequestHandler<DeleteCategoryCommand>.Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        return _categoryRepository.DeleteCategoryAsync(new ObjectId(request.Id));
    }
}