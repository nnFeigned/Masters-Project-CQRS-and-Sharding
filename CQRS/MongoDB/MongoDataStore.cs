using CQRS.Domain.Entities;

using MongoDB.Driver;

namespace CQRS.MongoDB;

public class MongoDataStore
{
    private readonly IMongoCollection<Product> _productCollection;

    public MongoDataStore(IMongoDatabase database)
    {
        _productCollection = database.GetCollection<Product>("Warehouse");
    }

    public async Task AddProduct(Product product) => await _productCollection.InsertOneAsync(product);

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        var products = await _productCollection.Find(Builders<Product>.Filter.Empty).ToListAsync();

        return products;
    }
}