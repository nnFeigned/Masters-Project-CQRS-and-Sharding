using CQRS.Application.Categories.Queries;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

namespace CQRS.Application.Categories.QueryHandlers;

public class GetCategoryByIdQueryHandler(IReadRepository<Category> categoryRepository) : IRequestHandler<GetCategoryByIdQuery, Category?>
{
    public async Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetEntityByIdAsync(request.Id);

        return category;
    }
}