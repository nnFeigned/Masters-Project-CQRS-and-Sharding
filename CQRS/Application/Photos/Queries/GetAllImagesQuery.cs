using CQRS.Domain.Entities;

using MediatR;

namespace CQRS.Application.Photos.Queries;

public class GetAllImagesQuery : IRequest<IEnumerable<Image>>
{
}