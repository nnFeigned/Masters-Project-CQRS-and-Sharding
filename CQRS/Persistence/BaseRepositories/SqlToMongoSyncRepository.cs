using CQRS.Domain.Entities;
using CQRS.Persistence.Context;
using Microsoft.EntityFrameworkCore;

using MongoDB.Driver;

namespace CQRS.Persistence.BaseRepositories;

public abstract class SqlToMongoSyncRepository<T>(ShopDbContext dbContext, IMongoCollection<T> collection) : ISyncRepository<T>
    where T : BaseEntity
{
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();

    public virtual async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetEntityByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<T> AddEntityAsync(T entity)
    {
        await collection.InsertOneAsync(entity);
        return entity;
    }

    public virtual async Task UpsertEntityAsync(T entity)
    {
        await collection.ReplaceOneAsync(document => document.Id == entity.Id, entity, new ReplaceOptions { IsUpsert = true });
    }

    public virtual async Task DeleteEntityAsync(Guid id)
    {
        await collection.DeleteOneAsync(document => document.Id == id);
    }
}