namespace CQRS.Domain.Repository
{
    public interface IWriteRepository<T>
    {
        Task<T> AddEntityAsync(T entity);
        Task UpdateEntityAsync(T entity);
        Task DeleteEntityAsync(Guid id);
    }
}
