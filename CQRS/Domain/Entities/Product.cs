using MongoDB.Bson;

namespace CQRS.Domain.Entities;

public class Product
{
    public ObjectId Id { get; set; }


    /// <summary>
    /// Product Name
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Optional Product description
    /// </summary>
    public string? Description { get; set; }

    public Product() { }

    public Product(ObjectId id, string name, string? description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}