using MediatR;

namespace CQRS.Application.Categories.Commands;

public class DeleteCategoryCommand : IRequest
{
    public required string Id { get; set; }
}