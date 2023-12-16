using CQRS.Application.Production.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

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
            Name = request.Name,
            Description = request.Description
        };

        await _productRepository.AddAsync(product);

        return product;
    }
}