using CQRS.Domain.Entities;
using MongoDB.Bson;

namespace CQRS.Domain.Repository.Write
{
    public interface IProductRepository
    {
        Task<Product> AddEntityAsync(Product entity);
        Task UpdateEntityAsync(Product entity);
        Task DeleteEntityAsync(Guid id);
    }
}
