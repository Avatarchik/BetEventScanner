using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BetEventScanner.DataAccess.Providers
{
    public class MongoDbProvider : IDbProvider
    {
        private readonly IMongoDatabase _db;

        public MongoDbProvider()
        {
            var url ="mongodb://qweqwe:qweqwe123123@ds137749.mlab.com:37749/ajsdb";
            var client = new MongoClient(url);
            _db = client.GetDatabase("ajsdb");
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

        public T GetEntity<T>(string collectionName, int id)
        {
            var collection = _db.GetCollection<T>(collectionName);
            var filter = new BsonDocument("Id", id);
            var r = collection.Find(new BsonDocumentFilterDefinition<T>(filter));
            return default(T);
        }

        public IEnumerable<T> GetEntities<T>(string collectionName)
        {
            var res = new List<T>();
            var data = _db.GetCollection<T>(collectionName);
            res.AddRange(data.AsQueryable());
            return res;
        }

        public void InsertEntity<T>(string collectionName, T entity)
        {
            var collection = _db.GetCollection<T>(collectionName);
            collection.InsertOne(entity);
        }

        public void InsertEntities<T>(string collectionName, IEnumerable<T> entities)
        {
            var collection = _db.GetCollection<T>(collectionName);
            collection.InsertMany(entities);
        }

        public void UpdateEntity<T>(T entity)
        {
        }

        public void DeleteEntity(int id)
        {
        }
    }
}
