using CQRS.Domain.Entities;

using MongoDB.Driver;

namespace CQRS.Domain.Repository
{
    public class MongoReadRepository<T>(IMongoCollection<T> collection) : IReadRepository<T>
        where T : BaseEntity
    {
        public async Task<ICollection<T>> GetAllAsync()
        {
            return await collection.AsQueryable().ToListAsync();
        }

        public async Task<T> GetEntityByIdAsync(Guid id)
        {
            return await collection.Find(entity =>  entity.Id == id).FirstOrDefaultAsync();
        }
    }
}
