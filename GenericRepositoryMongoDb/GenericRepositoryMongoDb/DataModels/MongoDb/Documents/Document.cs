using MongoDB.Bson;

namespace GenericRepositoryMongoDb.DataModels.MongoDb.Documents
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
