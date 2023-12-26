namespace CQRS.Persistence.BaseRepositories;

public interface ISyncRepository<T>
{
    Task<T?> GetEntityByIdAsync(Guid id);

    Task<T> AddEntityAsync(T entity);
    Task UpsertEntityAsync(T entity);
    Task DeleteEntityAsync(Guid id);
}