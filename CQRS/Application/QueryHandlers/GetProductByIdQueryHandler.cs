using CQRS.Application.Queries;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

using MongoDB.Bson;

namespace CQRS.Application.QueryHandlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
{

    private readonly IProductRepository _productRepository;


    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var id = new ObjectId(request.Id);

        var product = await _productRepository.GetProductByIdAsync(id);

        return product;
    }
}