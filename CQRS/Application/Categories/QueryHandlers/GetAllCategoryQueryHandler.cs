using CQRS.Domain.Repository;
using CQRS.Domain.Entities;

using MediatR;
using CQRS.Application.Categories.Queries;

namespace CQRS.Application.Categories.QueryHandlers;

public class GetAllCategoryQueryHandler(IReadRepository<Category> categoryRepository) : IRequestHandler<GetAllCategoryQuery, IEnumerable<Category>>
{
    public async Task<IEnumerable<Category>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetAllAsync();
    }
}