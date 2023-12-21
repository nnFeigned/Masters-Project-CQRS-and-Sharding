using CQRS.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CQRS.Domain.Repository
{
    public class MongoReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoReadRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _collection.AsQueryable().ToListAsync();
        }

        public async Task<T> GetEntityByIdAsync(Guid id)
        {
            return await _collection.Find(entity =>  entity.Id == id).FirstOrDefaultAsync();
        }

    }
}
