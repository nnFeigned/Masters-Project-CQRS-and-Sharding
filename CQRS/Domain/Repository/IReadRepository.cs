using CQRS.Domain.Entities;
using MongoDB.Bson;

namespace CQRS.Domain.Repository
{
    public interface IReadRepository<T>
    {
        Task<ICollection<T>> GetAllAsync();

        Task<T> GetEntityByIdAsync(Guid id);
    }
}