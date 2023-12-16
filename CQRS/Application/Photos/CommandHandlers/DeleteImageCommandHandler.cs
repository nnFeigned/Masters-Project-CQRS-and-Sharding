﻿using CQRS.Application.Photos.Commands;
using CQRS.Application.Production.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

using MongoDB.Bson;

namespace CQRS.Application.Photos.CommandHandlers;

public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand>
{
    
    private readonly IWriteRepository<Image> _imagesRepository;


    public DeleteImageCommandHandler(IWriteRepository<Image> imagesRepository)
    {
        _imagesRepository = imagesRepository;
    }

    public async Task Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        var productId = new ObjectId(request.Id);
        await _imagesRepository.DeleteAsync(productId);
    }
}