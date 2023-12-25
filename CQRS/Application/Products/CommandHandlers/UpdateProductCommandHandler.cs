using CQRS.Application.Products.Commands;
using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;
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
            Description = request.Description,
            CategoryId = request.CategoryId

        };
        Image image;
        foreach(var imgFileName in request.fileNames)
        {
            image=new Image() { 
                Id= Guid.NewGuid(),
                FileName = imgFileName,
                ProductId = request.Id,
                Product = product
            };
            product.Images.Add(image);
        }
            await productRepository.UpdateEntityAsync(product);
    }
}