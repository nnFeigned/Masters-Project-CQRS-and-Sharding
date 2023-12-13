using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CQRS.Domain.Entities
{
    public class Category
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public  string  Name { get; set; }
        [BsonElement("ProductsId")]
        public  List<ObjectId> Products { get; set; }

    }
}
