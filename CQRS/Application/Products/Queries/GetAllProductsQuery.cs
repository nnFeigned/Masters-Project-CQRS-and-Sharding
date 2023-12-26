using CQRS.Domain.Entities;

using MediatR;

namespace CQRS.Application.Products.Queries;

public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
{
}