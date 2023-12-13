using CQRS.Domain.Entities;

using MediatR;

namespace CQRS.Application.Production.Queries;

public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
{
}