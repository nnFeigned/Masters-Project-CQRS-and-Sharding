using CQRS.Domain.Entities;

using MediatR;

namespace CQRS.Application.Categories.Queries;

public class GetCategoryByIdQuery : IRequest<Category?>
{
    public required Guid Id { get; set; }
}