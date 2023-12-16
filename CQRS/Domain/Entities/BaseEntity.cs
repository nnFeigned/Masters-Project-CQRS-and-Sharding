using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CQRS.Domain.Entities
{
    public class BaseEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
