using CQRS.Application.Item.Commands;
using CQRS.Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace CQRS.Application.Item.CommandHandlers
{
    public class DeleProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        public DeleProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        Task IRequestHandler<DeleteProductCommand>.Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return _productRepository.DeleteProductAsync(new ObjectId(request.id));
        }
    }
}
