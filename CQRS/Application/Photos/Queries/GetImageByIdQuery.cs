using CQRS.Domain.Entities;

using MediatR;

namespace CQRS.Application.Photos.Queries;

public class GetImageByIdQuery : IRequest<Image>
{
    public required string Id { get; set; }
}