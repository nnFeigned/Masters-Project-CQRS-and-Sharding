using Amazon.Runtime.Internal;
using CQRS.Application.Item.Queries;
using CQRS.Domain.Entitites;
using CQRS.Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace CQRS.Application.Item.QueryHandlers
{
    public class GetProductByIDHandler : IRequestHandler<GetProductByID, Product>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIDHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> Handle(GetProductByID request, CancellationToken cancellationToken)
        {
            var id=new ObjectId(request.Id);

            var product = await _productRepository.GetProductByIdAsync(id);

            return product;
        }
    }
}
