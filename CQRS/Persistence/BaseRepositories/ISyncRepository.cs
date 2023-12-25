namespace CQRS.Persistence.BaseRepositories;

public interface ISyncRepository<T> : IReadRepository<T>, IWriteRepository<T>;