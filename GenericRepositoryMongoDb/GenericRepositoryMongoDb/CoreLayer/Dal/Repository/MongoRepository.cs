using GenericRepositoryMongoDb.CoreLayer.Dal.Repository.Abstractions;
using GenericRepositoryMongoDb.DataModels.MongoDb.Attributes;
using GenericRepositoryMongoDb.DataModels.MongoDb.Documents;
using GenericRepositoryMongoDb.DataModels.MongoDb.Setting;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace GenericRepositoryMongoDb.CoreLayer.Dal.Repository
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument>
       where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public virtual IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public virtual IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            //return Task.Run(() => 
            return _collection.Find(filterExpression).FirstOrDefaultAsync();
            //);
        }

        public virtual TDocument FindById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            return _collection.Find(filter).SingleOrDefault();
        }

        public virtual async Task<TDocument> FindByIdAsync(string id)
        {
            //return Task.Run(() =>
            //{
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            return await _collection.Find(filter).SingleOrDefaultAsync();
            //});
        }


        public virtual ObjectId InsertOne(TDocument document)
        {
            _collection.InsertOne(document);
            return document.Id;
        }

        public virtual async Task<ObjectId> InsertOneAsync(TDocument document)
        {
            //Task.Run(() => { });
            await _collection.InsertOneAsync(document);
            return document.Id;
        }

        public ICollection<ObjectId> InsertMany(ICollection<TDocument> documents)
        {
            _collection.InsertMany(documents);
            return documents.Select(x => x.Id).ToList();
        }

        public virtual async Task<ICollection<ObjectId>> InsertManyAsync(ICollection<TDocument> documents)
        {
            await _collection.InsertManyAsync(documents);
            return documents.Select(x => x.Id).ToList();
        }

        public void ReplaceOne(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }

        public virtual async Task ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        public async Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {

            //Task.Run(() =>
            await _collection.FindOneAndDeleteAsync(filterExpression);
            //);
        }

        public void DeleteById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            _collection.FindOneAndDelete(filter);
        }

        public async Task DeleteByIdAsync(string id)
        {
            //return Task.Run(() =>
            //{
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            await _collection.FindOneAndDeleteAsync(filter);
            //});
        }

        public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }

        public async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            //return Task.Run(() =>
            await _collection.DeleteManyAsync(filterExpression);
            //); 
        }

        public ICollection<TDocument> FindByIds(List<string> ids)
        {
            var filterDef = new FilterDefinitionBuilder<TDocument>();
            var filter = filterDef.In(x => nameof(x.Id), ids);

            return _collection.Find(filter).ToList();

        }

        public virtual async Task<ICollection<TDocument>> FindByIdsAsync(List<string> ids)
        {
            var filterDef = new FilterDefinitionBuilder<TDocument>();
            var filter = filterDef.In(x => nameof(x.Id), ids);

            var result = await _collection.FindAsync(filter);
            return result.ToList();
        }
    }
}
