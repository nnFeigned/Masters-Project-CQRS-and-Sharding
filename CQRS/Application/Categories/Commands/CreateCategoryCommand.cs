using CQRS.Domain.Entities;

using MediatR;
using MongoDB.Bson;

namespace CQRS.Application.Categories.Commands;

public class CreateCategoryCommand : IRequest<Category>
{
    public  string Name { get; set; }
    public List<ObjectId> Products { get; set; } = new List<ObjectId>();
}