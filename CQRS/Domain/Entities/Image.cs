using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CQRS.Domain.Entities
{
    public class Image : BaseEntity
    {

        [BsonElement("imagePath")]
        public required string FileName { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}