using CQRS.Domain.Entitites;
using MongoDB.Bson;
using System;

namespace CQRS.Domain.Repository
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetAllAsync();

        Task<Product> GetProductByIdAsync(ObjectId productId);
        Task<Product> AddProductAsync(Product toCreate);

        Task UpdateProductAsync(ObjectId productId, string name, string description);

        Task DeleteProductAsync(ObjectId productId);
    };
}
