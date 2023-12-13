using CQRS.Application.Photos.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

namespace CQRS.Application.Photos.CommandHandlers;

public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, Image>
{

    private readonly IImagesRepository _imagesRepository;


    public CreateImageCommandHandler(IImagesRepository imagesRepository)
    {
        _imagesRepository = imagesRepository;
    }

    public async Task<Image> Handle(CreateImageCommand request, CancellationToken cancellationToken)
    {
        var image = new Image
        {
            FileName = request.FileName
        };

        await _imagesRepository.AddImageAsync(image);

        return image;
    }
}