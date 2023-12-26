using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;

using MongoDB.Driver;

namespace CQRS.Persistence.Repositories;

public class CategoryReadRepository(IMongoCollection<Category> collection) : MongoReadRepository<Category>(collection);