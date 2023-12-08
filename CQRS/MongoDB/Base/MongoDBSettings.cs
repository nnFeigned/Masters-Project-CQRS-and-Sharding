namespace CQRS.MongoDB.Base
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = String.Empty;
        public string ProductDatabaseName { get; set; } = String.Empty;
    }
}