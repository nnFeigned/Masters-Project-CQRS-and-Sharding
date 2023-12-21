using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
    public string Description { get; set; }
    public Guid CategoryId { get; set; }

    //private List<Image> _images = new();


    //[BsonElement("ImageArrayId")]
    //public IReadOnlyList<Image> Images => _images;

    public virtual Category Category { get; set; }
    public virtual ICollection<Image> Images { get; set; }
    public Product() { }

    public Product(Guid id, string name, string? description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
    public Product(Guid id, string name, string description, Guid categoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        CategoryId = categoryId;
    }
}

