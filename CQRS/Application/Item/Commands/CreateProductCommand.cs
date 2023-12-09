using CQRS.Domain.Entitites;
using MediatR;
using System.Windows.Input;

namespace CQRS.Application.Item.Commands
{
    public class CreateProductCommand : IRequest <Product>
    {
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
