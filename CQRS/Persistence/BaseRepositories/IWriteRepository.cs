namespace CQRS.Persistence.BaseRepositories;

public interface IWriteRepository<T>
{
    Task<T> AddEntityAsync(T entity);
    Task UpsertEntityAsync(T entity);
    Task DeleteEntityAsync(Guid id);
}