using CQRS.Application.Products.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

namespace CQRS.Application.Products.CommandHandlers;

public class UpdateProductCommandHandler(IWriteRepository<Product> productRepository) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description
        };

        await productRepository.UpdateEntityAsync(product);

        // Add images here
    }
}