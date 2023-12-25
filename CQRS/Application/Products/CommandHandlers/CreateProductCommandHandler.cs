using CQRS.Application.Products.Commands;
using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;
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
        Image image;
        foreach(var fileNameImg in request.fileNames)
        {
            image=new Image() { 
                Id = Guid.NewGuid(), 
                ProductId = product.Id, 
                FileName = fileNameImg,
                Product = product
            };
            product.Images.Add(image);
        }
        await productRepository.AddEntityAsync(product);

        return product;
    }
}