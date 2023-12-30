using CQRS.Domain.Entities;

using MediatR;

namespace CQRS.Application.Products.Queries;

public class GetProductByIdQuery : IRequest<Product?>
{
    public  Guid Id { get; set; }
}