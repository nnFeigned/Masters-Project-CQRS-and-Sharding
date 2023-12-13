using CQRS.Domain.Repository;
using CQRS.Domain.Entities;

using MediatR;
using CQRS.Application.Photos.Queries;

namespace CQRS.Application.Photos.QueryHandlers;

public class GetAllImageQueryHandler : IRequestHandler<GetAllImagesQuery, IEnumerable<Image>>
{

    private readonly IImagesRepository _imageRepository;

    public GetAllImageQueryHandler(IImagesRepository imagesRepository)
    {
        _imageRepository = imagesRepository;
    }

    public async Task<IEnumerable<Image>> Handle(GetAllImagesQuery request, CancellationToken cancellationToken)
    {
        return await _imageRepository.GetAllAsync();
    }
}