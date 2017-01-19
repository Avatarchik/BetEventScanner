using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BetEventScanner.DataAccess.Providers
{
    public class MongoDbProvider : IDbProvider
    {
        private MongoClient _client;
        private IMongoDatabase _db;

        public MongoDbProvider()
        {
            _client = new MongoClient();
            // ToDo define db name in appconfig
            _db = _client.GetDatabase("footballdb");
        }

        public void CreateCollection(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var collections = _db.ListCollections(new ListCollectionsOptions { Filter = filter });

            if (collections.ToList().Any())
            {
                return;
            }
             
            _db.CreateCollection(collectionName);   
        }

        public void InsertDocumentToCollection<T>(string collectionName, T document)
        {
            var collection = _db.GetCollection<T>(collectionName);
            collection.InsertOne(document);
        }

        public void InsertDocumentsToCollection<T>(string collectionName, IEnumerable<T> documents)
        {
            var collection = _db.GetCollection<T>(collectionName);
            collection.InsertMany(documents);
        }

        public void GetEntity<T>(string collectionName, int id)
        {
            var collection = _db.GetCollection<T>(collectionName);
            var filter = new BsonDocument("Id", id);
            var r = collection.Find(new BsonDocumentFilterDefinition<T>(filter));
        }

        public void GetEntities<T>(string collectionName)
        {
        }

        public void InsertEntity<T>(T entity)
        {
        }

        public void InsertEntities<T>(IEnumerable<T> entities)
        {
        }

        public void UpdateEntity<T>(T entity)
        {
        }

        public void DeleteEntity(int id)
        {
        }
    }
}
