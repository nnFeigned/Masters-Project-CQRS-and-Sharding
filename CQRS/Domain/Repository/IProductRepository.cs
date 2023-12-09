using CQRS.Domain.Entities;

using MongoDB.Bson;

namespace CQRS.Domain.Repository;

public interface IProductRepository
{
    Task<ICollection<Product>> GetAllAsync();

    Task<Product> GetProductByIdAsync(ObjectId productId);

    Task<Product> AddProductAsync(Product product);

    Task UpdateProductAsync(ObjectId productId, string name, string description);

    Task DeleteProductAsync(ObjectId productId);
};