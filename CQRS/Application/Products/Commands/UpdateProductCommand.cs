using MediatR;

namespace CQRS.Application.Products.Commands;

public class UpdateProductCommand : IRequest
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    // Add images here
}