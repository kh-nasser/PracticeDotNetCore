using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using mongoDbTest.Entity;

namespace mongoDbTest.Utils
{
    public class MongoDbTestService
    {
        const string connectionString = "mongodb://localhost:27017";
        const string dbName = "TestDb1";

        private IMongoDatabase GetDatabase()
        {
            MongoClient mongoClient = new MongoClient(connectionString);
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(dbName);
            return mongoDatabase;
        }

        private IMongoCollection<Collection> GetCollection<Collection>(string collectionName)
        {
            IMongoDatabase mongoDatabase = GetDatabase();
            IMongoCollection<Collection> collection = mongoDatabase.GetCollection<Collection>(collectionName);

            return collection;
        }

        public void ReadAllUserCollection()
        {
            IMongoCollection<BsonDocument> collection = GetCollection<BsonDocument>("User");

            //foreach (BsonDocument doc in await collection.Find(Builders<BsonDocument>.Filter.Empty).ToListAsync())
            foreach (BsonDocument doc in collection.Find(Builders<BsonDocument>.Filter.Empty).ToCursor().ToEnumerable())
            {
                Console.WriteLine(doc.ToString());
            }
        }

        public User Get(string id)
        {
            var result = GetCollection<User>("User")
                .Find(q => q.Id == ObjectId.Parse(id))
                .FirstOrDefault();

            return result;
        }

        public async Task ReadUserCollection(int age)
        {
            IMongoCollection<User> collection = GetCollection<User>("User");

            var filter = Builders<User>.Filter.Eq<int>(nameof(User.Age), age);
            var prettyPrintJsonOption = new JsonSerializerOptions { WriteIndented = true };

            await collection.Find(filter)
                .ForEachAsync(document => Console.WriteLine(JsonSerializer.Serialize(document, prettyPrintJsonOption)));
        }

        public async Task InsertBsonDocument()
        {
            IMongoCollection<BsonDocument> collection = GetCollection<BsonDocument>("User");

            var doc = new BsonDocument() {
                {"name", "Ted"},
                {"age", 25}
            };

            await collection.InsertOneAsync(doc);
        }
        public async Task InsertEntity(User doc)
        {
            IMongoCollection<User> collection = GetCollection<User>("User");
            await collection.InsertOneAsync(doc);
        }
        public async Task InsertEntity()
        {
            IMongoCollection<User> collection = GetCollection<User>("User");

            User docUser = new User()
            {
                Age = 25,
                Name = "Ada"
            };

            await collection.InsertOneAsync(docUser);
        }

        public async Task RemoveUserCollection()
        {
            IMongoCollection<User> collection = GetCollection<User>("User");

            var filter = Builders<User>.Filter.Eq<ObjectId>(nameof(User.Id), ObjectId.Parse("62b23019352a1001e24c3af6"));

            await collection.DeleteOneAsync(filter);
        }

        public async Task UpdateUserCollection(string id, string name, int age)
        {
            IMongoCollection<User> collection = GetCollection<User>("User");

            var filter = Builders<User>.Filter.Where(x => x.Id == ObjectId.Parse(id));
            var update = Builders<User>
                .Update
                .Set(nameof(User.Name), name)
                .Set(nameof(User.Age), age);

            await collection.UpdateOneAsync(filter, update);
        }

        public async Task ReadWithLinq()
        {
            IMongoCollection<User> collection = GetCollection<User>("User");

            var result =
                (from c in collection.AsQueryable<User>()
                     //where c.Age > 25
                 orderby c.Age descending
                 select c);

            var count = result.Count();

            Console.WriteLine("count: {0} ", count);
            foreach (var doc in result)
            {
                Console.WriteLine(doc.Name);
            }
        }
    }
}