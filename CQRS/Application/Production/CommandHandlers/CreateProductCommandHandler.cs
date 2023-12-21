using CQRS.Application.Production.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;
using CQRS.Domain.Repository.Write;
using MediatR;

namespace CQRS.Application.Production.CommandHandlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{

    private readonly IWriteRepository<Product> _productRepository;


    public CreateProductCommandHandler(IWriteRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description
        };

        await _productRepository.AddEntityAsync(product);

        return product;
    }
}