using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;
using CQRS.Persistence.Context;

namespace CQRS.Persistence.Repositories;

public class CategoryWriteRepository(ShopDbContext dbContext) : SqlWriteRepository<Category>(dbContext);