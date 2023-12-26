namespace CQRS.Persistence.BaseRepositories;

public interface IReadRepository<T>
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetEntityByIdAsync(Guid id);
}