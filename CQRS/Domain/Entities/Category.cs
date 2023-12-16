using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CQRS.Domain.Entities
{
    public class Category : BaseEntity
    {

        public  string  Name { get; set; }

        [BsonElement("ProductsId")]
        public List<ObjectId> Products { get; set; } = new List<ObjectId>();
    }

}
