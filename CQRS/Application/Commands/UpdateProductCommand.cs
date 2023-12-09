using MediatR;

namespace CQRS.Application.Commands;

public class UpdateProductCommand : IRequest
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}