using CQRS.Application.Products.Queries;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

namespace CQRS.Application.Products.QueryHandlers;

public class GetAllProductsQueryHandler(IReadRepository<Product> productRepository) : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
{
    public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await productRepository.GetAllAsync();
    }
}