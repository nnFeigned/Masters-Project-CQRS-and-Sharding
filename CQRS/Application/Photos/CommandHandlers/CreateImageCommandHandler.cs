using CQRS.Application.Photos.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;
using CQRS.Domain.Repository.Write;
using MediatR;

namespace CQRS.Application.Photos.CommandHandlers;

public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, Image>
{

    private readonly IWriteRepository<Image> _imagesRepository;


    public CreateImageCommandHandler(IWriteRepository<Image> imagesRepository)
    {
        _imagesRepository = imagesRepository;
    }

    public async Task<Image> Handle(CreateImageCommand request, CancellationToken cancellationToken)
    {
        var image = new Image
        {
            Id = Guid.NewGuid(),
            FileName = request.FileName
        };

        await _imagesRepository.AddEntityAsync(image);

        return image;
    }
}