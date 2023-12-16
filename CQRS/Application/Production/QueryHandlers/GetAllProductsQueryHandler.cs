using CQRS.Domain.Repository;
using CQRS.Domain.Entities;

using MediatR;
using CQRS.Application.Production.Queries;

namespace CQRS.Application.Production.QueryHandlers;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
{

    private readonly IReadRepository<Product> _productRepository;

    public GetAllProductsQueryHandler(IReadRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetAllAsync();
    }
}