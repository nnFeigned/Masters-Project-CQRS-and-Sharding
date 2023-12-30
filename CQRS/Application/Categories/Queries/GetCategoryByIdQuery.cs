using CQRS.Domain.Entities;

using MediatR;

namespace CQRS.Application.Categories.Queries;

public class GetCategoryByIdQuery : IRequest<Category?>
{
    public  Guid Id { get; set; }
}