using CQRS.Application.Categories.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;

using MediatR;

namespace CQRS.Application.Categories.CommandHandlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
{

    private readonly IWriteRepository<Category> _categoryRepository;


    public CreateCategoryCommandHandler(IWriteRepository<Category> categoryRepository)
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

        await _categoryRepository.AddAsync(category);

        return category;
    }
}