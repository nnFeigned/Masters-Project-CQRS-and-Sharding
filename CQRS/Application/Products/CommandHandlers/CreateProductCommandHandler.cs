using CQRS.Application.Products.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

namespace CQRS.Application.Products.CommandHandlers;

public class CreateProductCommandHandler(IWriteRepository<Product> productRepository) : IRequestHandler<CreateProductCommand, Product>
{
    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            CategoryId = request.CategoryId
        };

        await productRepository.AddEntityAsync(product);

        return product;


    }
}