using CQRS.Application.Production.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

using MongoDB.Bson;

namespace CQRS.Application.Production.CommandHandlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{

    private readonly IWriteRepository<Product> _productRepository;

    public UpdateProductCommandHandler(IWriteRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {

        var productId = new ObjectId(request.Id);

        var Produc =new Product
        {
            Id = productId,
            Name = request.Name,
            Description = request.Description
        };

        await _productRepository.UpdateAsync(Produc);
    }
}