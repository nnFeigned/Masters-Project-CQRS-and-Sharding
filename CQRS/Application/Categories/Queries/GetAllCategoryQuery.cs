using CQRS.Domain.Entities;

using MediatR;

namespace CQRS.Application.Categories.Queries;

public class GetAllCategoryQuery : IRequest<IEnumerable<Category>>
{
}