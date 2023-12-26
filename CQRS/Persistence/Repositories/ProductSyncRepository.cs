using CQRS.Domain.Entities;
using CQRS.Persistence.BaseRepositories;
using CQRS.Persistence.Context;

using Microsoft.EntityFrameworkCore;

using MongoDB.Driver;

namespace CQRS.Persistence.Repositories;

public class ProductSyncRepository(ShopDbContext dbContext, IMongoCollection<Product> collection) : SqlToMongoSyncRepository<Product>(dbContext, collection)
{
    public override Task<Product?> GetEntityByIdAsync(Guid id)
    {
        return dbContext.Products
            .Include(product => product.Images)
            .AsNoTracking()
            .FirstOrDefaultAsync(product => product.Id == id);
    }
}