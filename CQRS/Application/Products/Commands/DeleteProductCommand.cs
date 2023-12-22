using MediatR;

namespace CQRS.Application.Products.Commands;

public class DeleteProductCommand : IRequest
{
    public required Guid Id { get; set; }

    // Add images here
}