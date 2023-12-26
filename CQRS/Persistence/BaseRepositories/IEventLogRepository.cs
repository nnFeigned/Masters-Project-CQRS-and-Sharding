using CQRS.Domain.Entities;

namespace CQRS.Persistence.BaseRepositories;

public interface IEventLogRepository
{
    IQueryable<EventLog> GetAll();
    Task UpdateLog(EventLog eventLog);
}