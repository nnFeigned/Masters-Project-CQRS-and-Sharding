using CQRS.Application.Categories.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;
using CQRS.Domain.Repository.Write;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Categories.CommandHandlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
{

    //private readonly ICategoryRepository _categoryRepository;

    private readonly IWriteRepository<Category> _categoryRepository;
    public CreateCategoryCommandHandler(IWriteRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            ProductIds = request.Products
        };

        await _categoryRepository.AddEntityAsync(category);

        return category;
    }
}