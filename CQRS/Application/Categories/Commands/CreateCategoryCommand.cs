using CQRS.Domain.Entities;

using MediatR;

namespace CQRS.Application.Categories.Commands;

public class CreateCategoryCommand : IRequest<Category>
{
    public required string Name { get; set; }

    // Do not create products here
}