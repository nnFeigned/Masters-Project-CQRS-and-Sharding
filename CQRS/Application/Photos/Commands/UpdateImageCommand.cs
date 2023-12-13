
using MediatR;

namespace CQRS.Application.Photos.Commands;

public class UpdateImageCommand : IRequest
{
    public required string Id { get; set; }
    public required string FileName { get; set; }

}