using CQRS.Domain.Entities;
using MongoDB.Bson;

namespace CQRS.Domain.Repository.Write
{
    public interface ICategoryRepository
    {
        Task<Category> AddEntityAsync(Category entity);
        Task UpdateEntityAsync(Category entity);
        Task DeleteEntityAsync(Guid id);
    }
}
