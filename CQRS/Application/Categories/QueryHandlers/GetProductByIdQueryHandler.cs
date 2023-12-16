using CQRS.Application.Categories.Queries;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

using MongoDB.Bson;

namespace CQRS.Application.Categories.QueryHandlers;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
{

    private readonly IReadRepository<Category> _categoryRepository;


    public GetCategoryByIdQueryHandler(IReadRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var id = new ObjectId(request.Id);

        var product = await _categoryRepository.GetEntityByIdAsync(id);

        return product;
    }
}