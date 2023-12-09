using MediatR;

namespace CQRS.Application.Item.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public required string id { get; set; }
    }
}
