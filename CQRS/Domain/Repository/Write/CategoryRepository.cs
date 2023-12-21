using CQRS.DataContext;
using CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace CQRS.Domain.Repository.Write
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MagazineDbContext _dbContext;
        public CategoryRepository(MagazineDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Category> AddEntityAsync(Category entity)
        {
            await _dbContext.AddAsync(entity);
            _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteEntityAsync(Guid id)
        {
            _dbContext.Remove(id);
        }

        public async Task UpdateEntityAsync(Category entity)
        {
            _dbContext.Update(entity);
        }
    }
}
