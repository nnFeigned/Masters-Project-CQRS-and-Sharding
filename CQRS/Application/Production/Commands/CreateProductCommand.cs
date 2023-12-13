using CQRS.Domain.Entities;

using MediatR;

namespace CQRS.Application.Production.Commands;

public class CreateProductCommand : IRequest<Product>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}