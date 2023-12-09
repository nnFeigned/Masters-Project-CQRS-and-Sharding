using CQRS.Domain.Entitites;
using MediatR;

namespace CQRS.Application.Item.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {

    }
}
