using CQRS.Application.Categories.Commands;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;
using CQRS.Domain.Repository.Write;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace CQRS.Application.Categories.CommandHandlers;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{

    private readonly IWriteRepository<Category> _categoryRepository;

    public UpdateCategoryCommandHandler(IWriteRepository<Category> CategoryRepository)
    {
        _categoryRepository = CategoryRepository;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {


        var Categor = new Category
        {
            Name = request.Name,
            ProductIds = request.Products
        };

        await _categoryRepository.UpdateEntityAsync(Categor);
    }
}