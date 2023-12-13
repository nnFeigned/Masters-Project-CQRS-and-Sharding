using CQRS.Application.Categories.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

namespace CQRS.Application.Categories.CommandHandlers;

public class CreateImageCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
{

    private readonly ICategoryRepository _categoryRepository;


    public CreateImageCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
            Products = request.Products
        };

        await _categoryRepository.AddCategoryAsync(category);

        return category;
    }
}