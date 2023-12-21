using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CQRS.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
