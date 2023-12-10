using MediatR;

namespace CQRS.Application.Commands;

public class DeleteProductCommand : IRequest
{
    public required string Id { get; set; }
}