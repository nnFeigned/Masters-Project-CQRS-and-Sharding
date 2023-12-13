using CQRS.Application.Photos.Commands;
using CQRS.Domain.Repository;

using MediatR;

using MongoDB.Bson;

namespace CQRS.Application.Photos.CommandHandlers;

public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand>
{
    
    private readonly IImagesRepository _imagesRepository;


    public DeleteImageCommandHandler(IImagesRepository imagesRepository)
    {
        _imagesRepository = imagesRepository;
    }

    Task IRequestHandler<DeleteImageCommand>.Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        return _imagesRepository.DeleteImageAsync(new ObjectId(request.Id));
    }
}