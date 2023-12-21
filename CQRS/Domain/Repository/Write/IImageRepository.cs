using CQRS.Domain.Entities;
using MongoDB.Bson;

namespace CQRS.Domain.Repository.Write
{
    public interface IImageRepository
    {
        Task<Image> AddEntityAsync(Image entity);
        Task UpdateEntityAsync(Image entity);
        Task DeleteEntityAsync(Guid id);
    }
}
