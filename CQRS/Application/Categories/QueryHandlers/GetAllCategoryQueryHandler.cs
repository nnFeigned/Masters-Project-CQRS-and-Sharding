using CQRS.Domain.Repository;
using CQRS.Domain.Entities;

using MediatR;
using CQRS.Application.Categories.Queries;

namespace CQRS.Application.Categories.QueryHandlers;

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, IEnumerable<Category>>
{

    private readonly IReadRepository<Category> _categoryRepository;

    public GetAllCategoryQueryHandler(IReadRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetAllAsync();
    }
}