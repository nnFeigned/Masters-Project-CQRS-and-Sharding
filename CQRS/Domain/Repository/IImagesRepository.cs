using CQRS.Domain.Entities;
using MongoDB.Bson;

namespace CQRS.Domain.Repository
{
    public interface IImagesRepository
    {
        Task<ICollection<Image>> GetAllAsync();

        Task<Image> GetImageByIdAsync(ObjectId imageId);

        Task<Image> AddImageAsync(Image image);

        Task UpdateImageAsync(ObjectId imageId, string name);

        Task DeleteImageAsync(ObjectId imageId);
    }
}
