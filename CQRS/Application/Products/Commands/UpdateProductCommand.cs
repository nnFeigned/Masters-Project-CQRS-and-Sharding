using MediatR;

namespace CQRS.Application.Products.Commands;

public class UpdateProductCommand : IRequest
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public Guid CategoryId { get; set; }
    public List<string> fileNames { get; set; } = new();
}