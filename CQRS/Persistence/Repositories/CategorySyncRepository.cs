using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;
using CQRS.Persistence.Context;

using MongoDB.Driver;

namespace CQRS.Persistence.Repositories;

public class CategorySyncRepository(ShopDbContext dbContext, IMongoCollection<Category> collection) : SqlToMongoSyncRepository<Category>(dbContext, collection);