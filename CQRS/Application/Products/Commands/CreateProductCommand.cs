using CQRS.Domain.Entities;

using MediatR;

namespace CQRS.Application.Products.Commands;

public class CreateProductCommand : IRequest<Product>
{
    public required string Name { get; set; }
    public string? Description { get; set; }

    public Guid CategoryId { get; set; }

    public List<string> fileNames { get; set; }
}