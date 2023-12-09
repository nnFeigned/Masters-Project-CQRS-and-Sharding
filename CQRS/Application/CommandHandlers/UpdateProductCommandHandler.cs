using CQRS.Application.Commands;
using CQRS.Domain.Repository;

using MediatR;

using MongoDB.Bson;

namespace CQRS.Application.CommandHandlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;
    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {

        var productId = new ObjectId(request.Id);

        await _productRepository.UpdateProductAsync(productId, request.Name, request.Description);
    }
}