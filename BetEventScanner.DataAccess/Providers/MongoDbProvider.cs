using System.Collections.Generic;
using System.Linq;
using BetEventScanner.DataAccess.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BetEventScanner.DataAccess.Providers
{
    public class MongoDbProvider : IDbProvider
    {
        private readonly IMongoDatabase _db;

        public MongoDbProvider(string dbName)
        {
            //var url ="mongodb://qweqwe:qweqwe123123@ds137749.mlab.com:37749/ajsdb";
            //var client = new MongoClient(url);
            var client = new MongoClient();
            _db = client.GetDatabase(dbName);
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

        public void Insert<T>(string collectionName, T document)
        {
            var collection = _db.GetCollection<T>(collectionName);
            collection.InsertOne(document);
        }

        public void InsertMany<T>(string collectionName, IEnumerable<T> documents)
        {
            var collection = _db.GetCollection<T>(collectionName);
            collection.InsertMany(documents);
        }

        public T Get<T>(string collectionName, int id)
        {
            var collection = _db.GetCollection<T>(collectionName);
            var filter = new BsonDocument("MatchId", id);
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

        public void UpdateEntity<T>(string collectionName, T entity) where T : IDocEntity
        {
            var collection = _db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq(s => s.Id, entity.Id);
            var result = collection.ReplaceOne(filter, entity);
        }

        public IEnumerable<T> GetMany<T>(string collectionName)
        {
            throw new System.NotImplementedException();
        }

        public void Update<T>(string collectionName, T entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
