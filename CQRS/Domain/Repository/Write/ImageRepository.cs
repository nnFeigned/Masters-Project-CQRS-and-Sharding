using CQRS.DataContext;
using CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace CQRS.Domain.Repository.Write
{
    public class ImageRepository : IImageRepository
    {
        private readonly MagazineDbContext _dbContext;
        public ImageRepository(MagazineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Image> AddEntityAsync(Image entity)
        {
            await _dbContext.AddAsync(entity);
            _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteEntityAsync(Guid id)
        {
            _dbContext.Remove(id);
        }

        public async Task UpdateEntityAsync(Image entity)
        {
            _dbContext.Update(entity);
        }
    }
}
