using CQRS.Application.Photos.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

using MongoDB.Bson;

namespace CQRS.Application.Photos.CommandHandlers;

public class UpdateImageCommandHandler : IRequestHandler<UpdateImageCommand>
{

    private readonly IWriteRepository<Image> _imagesRepository;

    public UpdateImageCommandHandler(IWriteRepository<Image> imagesRepository)
    {
        _imagesRepository = imagesRepository;
    }

    public async Task Handle(UpdateImageCommand request, CancellationToken cancellationToken)
    {

        var CategoryId = new ObjectId(request.Id);

        var Image=new Image
        {
            Id = CategoryId,
            FileName = request.FileName
        };

        await _imagesRepository.UpdateAsync(Image);
    }
}