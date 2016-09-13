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
    }
}
