using CQRS.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using CQRS.DataContext;


namespace CQRS.Domain.Repository
{
    public class SQLWriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private IMongoCollection<T> coll;

        public SQLWriteRepository(IMongoCollection<T> dbContext)
        {
            coll = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            //_dbContext.Set<T>().Add(entity);
            //await _dbContext.SaveChangesAsync();
            //return entity;
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            //_dbContext.Set<T>().Update(entity);
            //await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ObjectId id)
        {
            //var entity = await _dbContext.Set<T>().FindAsync(id);
            //if (entity != null)
            //{
            //    _dbContext.Set<T>().Remove(entity);
            //    await _dbContext.SaveChangesAsync();
            //}
        }
    }
}
