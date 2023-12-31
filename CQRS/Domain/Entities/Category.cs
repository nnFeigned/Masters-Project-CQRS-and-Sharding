using MongoDB.Bson.Serialization.Attributes;

namespace CQRS.Domain.Entities;

public class Category : BaseEntity
{
    public required string Name { get; set; }

    [BsonElement("Products")]
    public virtual List<Product> Products { get; set; } = new();
}