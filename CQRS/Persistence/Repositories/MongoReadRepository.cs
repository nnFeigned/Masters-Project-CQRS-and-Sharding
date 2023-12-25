using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;
using MongoDB.Driver;

namespace CQRS.Persistence.Repositories;

public class MongoReadRepository<T>(IMongoCollection<T> collection) : IReadRepository<T>
    where T : BaseEntity
{
    public async Task<List<T>> GetAllAsync()
    {
        return await collection.AsQueryable().ToListAsync();
    }

    public async Task<T?> GetEntityByIdAsync(Guid id)
    {
        return await collection.Find(entity => entity.Id == id).FirstOrDefaultAsync();
    }
}