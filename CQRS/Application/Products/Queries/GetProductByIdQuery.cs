using CQRS.Domain.Entities;

using MediatR;

namespace CQRS.Application.Products.Queries;

public class GetProductByIdQuery : IRequest<Product?>
{
    public required Guid Id { get; set; }
}