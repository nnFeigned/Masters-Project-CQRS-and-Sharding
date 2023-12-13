using CQRS.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CQRS.Domain.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _category;

        public CategoryRepository(IMongoCollection<Category> _collection)
        {
            _category = _collection;
        }

        public async Task<ICollection<Category>> GetAllAsync()
        {
            return await _category.AsQueryable().ToListAsync();
        }


        public async Task<Category> AddCategoryAsync(Category category)
        {
            await _category.InsertOneAsync(category);
            return category;
        }

        public async Task DeleteCategoryAsync(ObjectId categoryId)
        {
            var filter = Builders<Category>.Filter
                .Eq(category => category.Id, categoryId);
            await _category.DeleteOneAsync(filter);
        }

        public async Task<Category> GetCategoryByIdAsync(ObjectId id)
        {
            return await _category.Find(category => category.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateCategoryAsync(ObjectId id, string name, List<ObjectId> list)
        {
            var filter = Builders<Category>.Filter
                .Eq(u => u.Id, id);

            var update = Builders<Category>.Update
                .Set(category => category.Name, name)
                .Set(category => category.Products, list);

            await _category.UpdateOneAsync(filter, update);
        }
    }
}
