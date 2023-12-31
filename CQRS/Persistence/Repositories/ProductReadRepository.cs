using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;
using MongoDB.Driver;

namespace CQRS.Persistence.Repositories;

public class ProductReadRepository(IMongoCollection<Product> collection) : MongoReadRepository<Product>(collection);