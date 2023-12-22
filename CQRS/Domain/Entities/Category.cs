using MongoDB.Bson.Serialization.Attributes;

namespace CQRS.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        [BsonElement("Products")]
        public virtual ICollection<Product> Products { get; set; }
    }

}
