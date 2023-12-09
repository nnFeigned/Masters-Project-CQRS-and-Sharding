using CQRS.Domain.Entities;

using MediatR;

namespace CQRS.Application.Queries;

public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
{
}