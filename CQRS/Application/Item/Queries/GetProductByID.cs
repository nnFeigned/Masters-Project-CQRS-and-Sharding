using CQRS.Domain.Entitites;
using MediatR;

namespace CQRS.Application.Item.Queries
{
    public class GetProductByID : IRequest<Product>
    {
        public required string Id { get; set; }
    }
}
