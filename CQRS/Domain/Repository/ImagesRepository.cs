using CQRS.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CQRS.Domain.Repository
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly IMongoCollection<Image> _image;

        public ImagesRepository(IMongoCollection<Image> image)
        {
            _image = image;
        }

        public async Task<Image> AddImageAsync(Image image)
        {
            await _image.InsertOneAsync(image);
            return image;
        }

        public async Task DeleteImageAsync(ObjectId imageId)
        {
            var filter = Builders<Image>.Filter
           .Eq(image => image.Id, imageId);
            await _image.DeleteOneAsync(filter);
        }

        public async Task<ICollection<Image>> GetAllAsync()
        {
           return await _image.Find(_ => true).ToListAsync();
        }

        public async Task<Image> GetImageByIdAsync(ObjectId imageId)
        {
            return await _image.Find(image => image.Id == imageId).FirstOrDefaultAsync();
        }

        public async Task UpdateImageAsync(ObjectId imageId, string name)
        {
            var filter = Builders<Image>.Filter
             .Eq(u => u.Id, imageId);

            var update = Builders<Image>.Update
                .Set(image => image.FileName, name);

            await _image.UpdateOneAsync(filter, update);
        }
    }
}
