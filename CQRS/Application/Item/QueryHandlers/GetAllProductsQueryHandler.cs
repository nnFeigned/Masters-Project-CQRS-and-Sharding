using CQRS.Application.Item.Queries;
using CQRS.Domain.Entitites;
using CQRS.Domain.Repository;
using MediatR;

namespace CQRS.Application.Item.QueryHandlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
          return await _productRepository.GetAllAsync();
        }
    }
}
