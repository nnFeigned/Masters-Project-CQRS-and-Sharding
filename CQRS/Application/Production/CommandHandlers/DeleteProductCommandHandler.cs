﻿using CQRS.Application.Production.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;
using CQRS.Domain.Repository.Write;
using MediatR;

using MongoDB.Bson;

namespace CQRS.Application.Production.CommandHandlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{

    private readonly IWriteRepository<Product> _productRepository;


    public DeleteProductCommandHandler(IWriteRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productId = new Guid(request.Id);
        //new ObjectId(request.Id);
        await _productRepository.DeleteEntityAsync(productId);
    }
}