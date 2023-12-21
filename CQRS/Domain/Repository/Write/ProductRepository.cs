using CQRS.DataContext;
using CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace CQRS.Domain.Repository.Write
{
    public class ProductRepository : IProductRepository
    {
        private readonly MagazineDbContext _dbContext;
        public ProductRepository(MagazineDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product> AddEntityAsync(Product entity)
        {
            await _dbContext.AddAsync(entity);
            _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteEntityAsync(Guid id)
        {
            _dbContext.Remove(id);
        }

        public async Task UpdateEntityAsync(Product entity)
        {
            _dbContext.Update(entity);
        }
    }
}
