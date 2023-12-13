using CQRS.Domain.Entities;
using CQRS.MongoDB.Base;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CQRS.MongoDB.Util
{
    public static class MongoService
    {
        public static IMongoCollection<T> GetMongoCollection<T>(IServiceProvider serviceProvider)
        {
            var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>();
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DataBaseName);
            return database.GetCollection<T>(GetCollectionName<T>(settings));
        }

        private static string GetCollectionName<T>(IOptions<MongoDBSettings> settings)
        {
            return typeof(T) switch
            {
                Type type when type == typeof(Product) => settings.Value.ProductCollectionName,
                Type type when type == typeof(Category) => settings.Value.CategoryCollectionName,
                Type type when type == typeof(Image) => settings.Value.ImageCollectionName,
                _ => throw new ArgumentException($"Unsupported type: {typeof(T)}"),
            };
        }
    }
}
