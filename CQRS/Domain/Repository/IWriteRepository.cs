using MongoDB.Bson;

namespace CQRS.Domain.Repository
{
    public interface IWriteRepository<T>
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(ObjectId id);

    }
}
