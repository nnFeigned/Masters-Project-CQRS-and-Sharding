using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CQRS.Domain.Entities
{
    public class Image
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("imagePath")]
        public required string FileName { get; set; }
    }
}