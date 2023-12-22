using CQRS.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using CQRS.DataContext;

namespace CQRS.Domain.Repository
{
    public class SqlWriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet;
        private readonly ShopDbContext _dbContext;

        public SqlWriteRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        public async Task<T> AddEntityAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateEntityAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEntityAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}