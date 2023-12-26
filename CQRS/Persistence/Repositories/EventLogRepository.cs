using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;
using CQRS.Persistence.Context;

using Microsoft.EntityFrameworkCore;

namespace CQRS.Persistence.Repositories;

public class EventLogRepository(ShopDbContext dbContext) : IEventLogRepository
{
    private readonly DbSet<EventLog> _dbSet = dbContext.Set<EventLog>();

    public IQueryable<EventLog> GetAll()
    {
        return _dbSet.AsQueryable();
    }

    public async Task UpdateLog(EventLog eventLog)
    {
        dbContext.Update(eventLog);
        await dbContext.SaveChangesAsync();
    }
}