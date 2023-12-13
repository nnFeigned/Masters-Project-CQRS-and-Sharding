using CQRS.Domain.Entities;
using MediatR;
using MongoDB.Bson;

namespace CQRS.Application.Categories.Commands;

public class UpdateCategoryCommand : IRequest
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required List<ObjectId>? Products { get; set; }

}