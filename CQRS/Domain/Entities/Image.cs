using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CQRS.Domain.Entities;

public class Image : BaseEntity
{

    [BsonElement("imagePath")]
    public required string FileName { get; set; }

    public Guid ProductId { get; set; }

    [JsonIgnore]
    public Product Product { get; set; }
}