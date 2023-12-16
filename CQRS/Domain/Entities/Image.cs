using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CQRS.Domain.Entities
{
    public class Image : BaseEntity
    {

        [BsonElement("imagePath")]
        public required string FileName { get; set; }
    }
}