using CQRS.Application.Categories.Queries;
using CQRS.Application.Photos.Queries;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

using MongoDB.Bson;

namespace CQRS.Application.Categories.QueryHandlers;

public class GetImageByIdQueryHandler : IRequestHandler<GetImageByIdQuery, Image>
{

    private readonly IReadRepository<Image> _imagesRepository;


    public GetImageByIdQueryHandler(IReadRepository<Image> imagesRepository)
    {
         _imagesRepository = imagesRepository;
    }
    public async Task<Image> Handle(GetImageByIdQuery request, CancellationToken cancellationToken)
    {
        var id = new Guid(request.Id);

        var product = await _imagesRepository.GetEntityByIdAsync(id);

        return product;
    }
}