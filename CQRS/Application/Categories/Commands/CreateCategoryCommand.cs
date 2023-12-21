using CQRS.Domain.Entities;

using MediatR;
using MongoDB.Bson;

namespace CQRS.Application.Categories.Commands;

public class CreateCategoryCommand : IRequest<Category>
{
    public  string Name { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
}