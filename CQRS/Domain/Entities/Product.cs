using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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

    public ObjectId? CategoryId { get; set; }

    [BsonElement("ImageArrayId")]
    public List<ObjectId>? imageslist { get; set; }

    public Product() { }

    public Product(ObjectId id, string name, string? description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
    public Product(ObjectId id, string name, string? description, ObjectId categoryId, List<ObjectId> images)
    {
        Id = id;
        Name = name;
        Description = description;
        CategoryId = categoryId;
        imageslist = images;
    }
}

