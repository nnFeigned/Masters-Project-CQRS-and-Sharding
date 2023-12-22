using MediatR;

namespace CQRS.Application.Categories.Commands;

public class DeleteCategoryCommand : IRequest
{
    public required Guid Id { get; set; }
}