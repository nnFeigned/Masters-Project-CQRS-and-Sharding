using MediatR;

namespace CQRS.Application.Commands;

public class DeleteProductCommand : IRequest
{
    public required string id { get; set; }
}