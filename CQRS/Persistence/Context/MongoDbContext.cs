using CQRS.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CQRS.Persistence.Context;

public static class MongoDbContext
{
    public static IMongoCollection<T> GetMongoCollection<T>(IServiceProvider serviceProvider)
    {
        var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>();
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);

        var collectionName = GetCollectionName<T>(settings);
        var collection = database.GetCollection<T>(collectionName);

        if (collection == null)
        {
            database.CreateCollection(collectionName);
        }

        return collection!;
    }

    private static string GetCollectionName<T>(IOptions<MongoDBSettings> settings)
    {
        return typeof(T) switch
        {
            { } type when type == typeof(Product) => settings.Value.ProductCollectionName,
            { } type when type == typeof(Category) => settings.Value.CategoryCollectionName,
            _ => throw new ArgumentException($"Unsupported type: {typeof(T)}"),
        };
    }
}