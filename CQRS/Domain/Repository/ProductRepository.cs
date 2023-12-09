using CQRS.Domain.Entitites;
using CQRS.MongoDB.Base;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data;

namespace CQRS.Domain.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _product;

        public ProductRepository(IOptions<MongoDBSettings> settingsOptions)
        {
            var client = new MongoClient(settingsOptions.Value.ConnectionString);
            var userDatabase = client.GetDatabase(settingsOptions.Value.ProductDatabaseName);

            _product = userDatabase.GetCollection<Product>("Warehouse");
        }


        public async Task<ICollection<Product>> GetAllAsync()
        {
            return await _product.AsQueryable().ToListAsync();
        }

        public async Task<Product> AddProductAsync(Product toCreate)
        {
            await _product.InsertOneAsync(toCreate);
            return toCreate;
        }



        public async Task DeleteProductAsync(ObjectId ProductId)
        {
            var filter = Builders<Product>.Filter
                        .Eq(u => u.Id, ProductId);

            await _product.DeleteOneAsync(filter);
        }




        public async Task<Product> GetProductByIdAsync(ObjectId id)
        {
            return await _product.Find(u => u.Id == id).FirstOrDefaultAsync();
        }


        public async Task UpdateProductAsync(ObjectId id, string name, string description)
        {
            var filter = Builders<Product>.Filter
            .Eq(u => u.Id, id);

            var update = Builders<Product>.Update
                .Set(u => u.Name, name)
                .Set(d=>d.Description,description);

            await _product.UpdateOneAsync(filter, update);

        }
    }
}

