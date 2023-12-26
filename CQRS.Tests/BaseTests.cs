using CQRS.Domain.Entities;
using CQRS.Persistence.Context;
using CQRS.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;

using MongoDB.Driver;

namespace CQRS.Tests;

[TestClass]
public class BaseTests
{
    private const string TestSqlConnectionString = "Data Source=.\\SQLEXPRESS;Database=CQRS_Test;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

    private static readonly MongoDBSettings TestMongoDbSettings = new()
    {
        ConnectionString = "mongodb://localhost:27017",
        DatabaseName = "CQRS_Test",
        CategoryCollectionName = "Categories",
        ProductCollectionName = "Products"
    };

    private static readonly IMongoClient Client;

    private static IMongoCollection<Category> Categories;
    private static IMongoCollection<Product> Products;

    // We want to recreate context each time
    protected static ShopDbContext ShopDbContext
    {
        get
        {
            var optionsBuilder = new DbContextOptionsBuilder<ShopDbContext>().UseSqlServer(TestSqlConnectionString);
            return new ShopDbContext(optionsBuilder.Options);
        }
    }

    protected static EventLogRepository EventLogRepository => new(ShopDbContext);

    protected static SqlWriteRepository<Category> WriteCategoryRepository => new(ShopDbContext);
    protected static SqlToMongoSyncRepository<Category> SyncCategoriesRepository => new(ShopDbContext, Categories);
    protected static MongoReadRepository<Category> ReadCategoryRepository => new(Categories);

    protected static SqlToMongoSyncRepository<Product> SyncProductsRepository => new(ShopDbContext, Products);
    protected static SqlWriteRepository<Product> WriteProductRepository => new(ShopDbContext);
    protected static MongoReadRepository<Product> ReadProductRepository => new(Products);


    static BaseTests()
    {
        Client = new MongoClient(TestMongoDbSettings.ConnectionString);

        var database = Client.GetDatabase(TestMongoDbSettings.DatabaseName);

        Categories = database.GetCollection<Category>(TestMongoDbSettings.CategoryCollectionName);
        if (Categories == null)
        {
            database.CreateCollection(TestMongoDbSettings.CategoryCollectionName);
        }

        Products = database.GetCollection<Product>(TestMongoDbSettings.ProductCollectionName);
        if (Products == null)
        {
            database.CreateCollection(TestMongoDbSettings.ProductCollectionName);
        }
    }

    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        ShopDbContext.Database.EnsureDeleted();
        ShopDbContext.Database.EnsureCreated();

        Client.DropDatabase(TestMongoDbSettings.DatabaseName);
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        ShopDbContext.Database.EnsureDeleted();
        Client.DropDatabase(TestMongoDbSettings.DatabaseName);
    }
}