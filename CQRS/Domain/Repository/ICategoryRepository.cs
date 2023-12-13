using CQRS.Domain.Entities;
using MongoDB.Bson;

namespace CQRS.Domain.Repository
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetAllAsync();

        Task<Category> GetCategoryByIdAsync(ObjectId categoryId);

        Task<Category> AddCategoryAsync(Category category);

        Task UpdateCategoryAsync(ObjectId categoryId, string name, List<ObjectId> listProducts);

        Task DeleteCategoryAsync(ObjectId categoryId);
    }
}
