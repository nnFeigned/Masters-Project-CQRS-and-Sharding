using CQRS.Application.Products.Queries;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

namespace CQRS.Application.Products.QueryHandlers;

public class GetProductByIdQueryHandler(IReadRepository<Product> productRepository) : IRequestHandler<GetProductByIdQuery, Product?>
{
    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetEntityByIdAsync(request.Id);

        return product;
    }
}