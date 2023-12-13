using CQRS.Application.Photos.Commands;
using CQRS.Domain.Repository;

using MediatR;

using MongoDB.Bson;

namespace CQRS.Application.Photos.CommandHandlers;

public class UpdateImageCommandHandler : IRequestHandler<UpdateImageCommand>
{

    private readonly IImagesRepository _imagesRepository;

    public UpdateImageCommandHandler(IImagesRepository imagesRepository)
    {
        _imagesRepository = imagesRepository;
    }

    public async Task Handle(UpdateImageCommand request, CancellationToken cancellationToken)
    {

        var CategoryId = new ObjectId(request.Id);

        await _imagesRepository.UpdateImageAsync(CategoryId, request.FileName);
    }
}