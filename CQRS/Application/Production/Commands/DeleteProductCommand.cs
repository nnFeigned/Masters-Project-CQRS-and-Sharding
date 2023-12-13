using MediatR;

namespace CQRS.Application.Production.Commands;

public class DeleteProductCommand : IRequest
{
    public required string Id { get; set; }
}