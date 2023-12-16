using CQRS.Application.Categories.Commands;
using CQRS.Application.Photos.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

using MongoDB.Bson;

namespace CQRS.Application.Categories.CommandHandlers;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    
    private readonly IWriteRepository<Category> _categoryRepository;


    public DeleteCategoryCommandHandler(IWriteRepository<Category> CategoryRepository)
    {
        _categoryRepository = CategoryRepository;
    }
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var productId = new ObjectId(request.Id);
        await _categoryRepository.DeleteAsync(productId);
    }
}