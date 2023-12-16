using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace CQRS.Domain.Entities;

public class Product : BaseEntity
{


    /// <summary>
    /// Product Name
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Optional Product description
    /// </summary>
    public string? Description { get; set; }

    public ObjectId? CategoryId { get; set; }

    private List<Image> _images = new();


    [BsonElement("ImageArrayId")]
    public IReadOnlyList<Image> Images => _images;

    public Product() { }

    public Product(ObjectId id, string name, string? description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
    public Product(ObjectId id, string name, string? description, ObjectId categoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        CategoryId = categoryId;
    }
}

