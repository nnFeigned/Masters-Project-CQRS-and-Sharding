using CQRS.Domain.Entitites;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Application.Item.Commands
{
    public class UpdateProductCommand : IRequest
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
