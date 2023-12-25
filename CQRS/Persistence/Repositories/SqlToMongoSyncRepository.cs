using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;
using CQRS.Persistence.Context;

using Microsoft.EntityFrameworkCore;

using MongoDB.Driver;

namespace CQRS.Persistence.Repositories;

public class SqlToMongoSyncRepository<T>(ShopDbContext dbContext, IMongoCollection<T> collection) : ISyncRepository<T>
    where T : BaseEntity
{
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetEntityByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> AddEntityAsync(T entity)
    {
        await collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task UpdateEntityAsync(T entity)
    {
        // todo: test
        await collection.UpdateOneAsync(document => document.Id == entity.Id, new ObjectUpdateDefinition<T>(entity));
    }

    public async Task DeleteEntityAsync(Guid id)
    {
        await collection.DeleteOneAsync(document => document.Id == id);
    }
}