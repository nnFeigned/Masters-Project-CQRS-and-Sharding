using CQRS.Domain.Entities;

using MediatR;

namespace CQRS.Application.Production.Queries;

public class GetProductByIdQuery : IRequest<Product>
{
    public required string Id { get; set; }
}