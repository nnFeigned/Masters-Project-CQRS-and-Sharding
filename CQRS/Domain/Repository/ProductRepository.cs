using CQRS.Domain.Entities;

using MongoDB.Bson;
using MongoDB.Driver;

namespace CQRS.Domain.Repository;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _product;

    public ProductRepository(IMongoCollection<Product> _collection)
    {
        _product = _collection;
    }

    public async Task<ICollection<Product>> GetAllAsync()
    {
        return await _product.AsQueryable().ToListAsync();
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        await _product.InsertOneAsync(product);
        return product;
    }

    public async Task DeleteProductAsync(ObjectId productId)
    {
        var filter = Builders<Product>.Filter
            .Eq(product => product.Id, productId);
        await _product.DeleteOneAsync(filter);
    }

    public async Task<Product> GetProductByIdAsync(ObjectId id)
    {
        return await _product.Find(product => product.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task UpdateProductAsync(ObjectId id, string name, string description)
    {
        var filter = Builders<Product>.Filter
            .Eq(u => u.Id, id);

        var update = Builders<Product>.Update
            .Set(product => product.Name, name)
            .Set(product => product.Description, description);

        await _product.UpdateOneAsync(filter, update);
    }
}