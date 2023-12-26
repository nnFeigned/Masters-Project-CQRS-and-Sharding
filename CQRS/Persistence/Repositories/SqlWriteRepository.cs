using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;
using CQRS.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Persistence.Repositories;

public class SqlWriteRepository<T>(ShopDbContext dbContext) : IWriteRepository<T>
    where T : BaseEntity
{
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();

    public virtual async Task<T> AddEntityAsync(T entity)
    {
        await _dbSet.AddAsync(entity);

        await dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpsertEntityAsync(T entity)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteEntityAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}