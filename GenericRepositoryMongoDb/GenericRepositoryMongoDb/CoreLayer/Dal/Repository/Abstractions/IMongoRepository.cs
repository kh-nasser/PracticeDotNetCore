using GenericRepositoryMongoDb.DataModels.MongoDb.Documents;
using MongoDB.Bson;
using System.Linq.Expressions;

namespace GenericRepositoryMongoDb.CoreLayer.Dal.Repository.Abstractions
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        IQueryable<TDocument> AsQueryable();

        IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression);

        TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression);

        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        TDocument FindById(string id);

        Task<TDocument> FindByIdAsync(string id);
        ICollection<TDocument> FindByIds(List<string> ids);
        Task<ICollection<TDocument>> FindByIdsAsync(List<string> ids);

        ObjectId InsertOne(TDocument document);

        Task<ObjectId> InsertOneAsync(TDocument document);

        ICollection<ObjectId> InsertMany(ICollection<TDocument> documents);

        Task<ICollection<ObjectId>> InsertManyAsync(ICollection<TDocument> documents);

        void ReplaceOne(TDocument document);

        Task ReplaceOneAsync(TDocument document);

        void DeleteOne(Expression<Func<TDocument, bool>> filterExpression);

        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        void DeleteById(string id);

        Task DeleteByIdAsync(string id);

        void DeleteMany(Expression<Func<TDocument, bool>> filterExpression);

        Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);
    }
}