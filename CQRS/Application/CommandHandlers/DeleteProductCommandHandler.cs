using CQRS.Application.Commands;
using CQRS.Domain.Repository;

using MediatR;

using MongoDB.Bson;

namespace CQRS.Application.CommandHandlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    Task IRequestHandler<DeleteProductCommand>.Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        return _productRepository.DeleteProductAsync(new ObjectId(request.id));
    }
}