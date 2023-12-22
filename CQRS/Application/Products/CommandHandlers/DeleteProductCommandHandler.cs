using CQRS.Application.Products.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

namespace CQRS.Application.Products.CommandHandlers;

public class DeleteProductCommandHandler(IWriteRepository<Product> productRepository) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await productRepository.DeleteEntityAsync(request.Id);
    }
}