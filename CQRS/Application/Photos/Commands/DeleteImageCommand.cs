using MediatR;

namespace CQRS.Application.Photos.Commands;

public class DeleteImageCommand : IRequest
{
    public required string Id { get; set; }
}