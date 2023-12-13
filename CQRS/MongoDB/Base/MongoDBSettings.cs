namespace CQRS.MongoDB.Base;

public class MongoDBSettings
{
    public string ConnectionString { get; set; } = String.Empty;
    public string DataBaseName { get; set; } = String.Empty;
    public string ProductCollectionName { get; set; } = String.Empty;
    public string CategoryCollectionName { get; set; } = String.Empty;
    public string ImageCollectionName { get; set; } = String.Empty;
}