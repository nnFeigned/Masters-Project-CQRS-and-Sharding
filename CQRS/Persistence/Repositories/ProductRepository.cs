using CQRS.Domain.Entities;
using CQRS.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Persistence.Repositories
{
    public class ProductRepository(ShopDbContext dbContext) : SqlWriteRepository<Product>(dbContext)
    {
        private readonly DbSet<Product> _productDbSet = dbContext.Set<Product>();
        private readonly DbSet<Image> _imageDbSet = dbContext.Set<Image>();

        public override async Task<Product> AddEntityAsync(Product entity)
        {
            await _productDbSet.AddAsync(entity);
            foreach (var image in entity.Images)
            {
                await _imageDbSet.AddAsync(image);
            }
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public override async Task UpdateEntityAsync(Product entity)
        {
            var existingProduct = await _productDbSet.FindAsync(entity.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = entity.Name;
                existingProduct.Description = entity.Description;
                existingProduct.CategoryId = entity.CategoryId;

                foreach (var existingImage in existingProduct.Images.ToList())
                {
                    _imageDbSet.Remove(existingImage);
                }

                foreach (var newImage in entity.Images)
                {
                    _imageDbSet.Add(newImage);
                }
            }

            await dbContext.SaveChangesAsync();
        }


        public override async Task DeleteEntityAsync(Guid id)
        {
            var product = await _productDbSet.FindAsync(id);

            if (product != null)
            {
                foreach (var image in product.Images.ToList())
                {
                    _imageDbSet.Remove(image);
                }

                _productDbSet.Remove(product);
                await dbContext.SaveChangesAsync();
            }
        }

    }
}
