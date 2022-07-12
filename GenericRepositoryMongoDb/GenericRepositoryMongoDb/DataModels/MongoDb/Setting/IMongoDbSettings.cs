namespace GenericRepositoryMongoDb.DataModels.MongoDb.Setting
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
