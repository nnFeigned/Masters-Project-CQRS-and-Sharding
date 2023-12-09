using CQRS.Domain.Entities;

using MediatR;

namespace CQRS.Application.Queries;

public class GetProductByIdQuery : IRequest<Product>
{
    public required string Id { get; set; }
}