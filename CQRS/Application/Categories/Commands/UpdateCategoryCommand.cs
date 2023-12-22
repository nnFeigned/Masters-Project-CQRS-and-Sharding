using CQRS.Domain.Entities;
using MediatR;

namespace CQRS.Application.Categories.Commands;

public class UpdateCategoryCommand : IRequest
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required List<Product>? Products { get; set; }
}