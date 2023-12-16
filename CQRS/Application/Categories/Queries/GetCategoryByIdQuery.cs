using CQRS.Domain.Entities;

using MediatR;

namespace CQRS.Application.Categories.Queries;

public class GetCategoryByIdQuery : IRequest<Category>
{
    public required string Id { get; set; }
}