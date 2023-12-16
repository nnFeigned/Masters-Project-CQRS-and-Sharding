using CQRS.Application.Production.Queries;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

using MongoDB.Bson;

namespace CQRS.Application.Production.QueryHandlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
{

    private readonly IReadRepository<Product> _productRepository;


    public GetProductByIdQueryHandler(IReadRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var id = new ObjectId(request.Id);

        var product = await _productRepository.GetEntityByIdAsync(id);

        return product;
    }
}