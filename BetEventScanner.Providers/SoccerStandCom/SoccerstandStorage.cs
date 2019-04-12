using System.Collections.Generic;
using System.IO;
using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataAccess.Providers;
using BetEventScanner.Providers.Contracts;
using BetEventScanner.Providers.SoccerStandCom.Model;
using Newtonsoft.Json;

namespace BetEventScanner.Providers.SoccerStandCom
{
    public class SoccerstandMongoStorage : IStorage, IParserStorage<SoccerstandData>
    {
        private readonly IDbProvider _provider;
        private string _collection;

        public SoccerstandMongoStorage()
        {
            _provider = new MongoDbProvider("footballdb");
        }

        public void StoreDataToCollection(string collectionName, SoccerstandData data)
        {
            _collection = collectionName;
            Store<SoccerstandData>(data);
        }

        public void StoreDataToCollection(string collectionName, ICollection<SoccerstandData> data)
        {
            _collection = collectionName;
            Store(data);
        }

        public void Store<T>(T data)
        {
            _provider.InsertEntity(_collection, data);
        }

        public void Store<T>(ICollection<T> data)
        {
            _provider.InsertEntities(_collection, data);
        }

        public SoccerstandData LoadOriginSource(string url)
        {
            return JsonConvert.DeserializeObject<SoccerstandData>(File.ReadAllText($@"C:\BetEventScanner\soccer_stand\{url.Replace("\\", "")}.json"));
        }

        public void Store(SoccerstandData data)
        {
            var name = StringEx.RemoveSpecialSymbols(data.Url);

            File.WriteAllText($@"C:\BetEventScanner\soccer_stand\{name}.json", JsonConvert.SerializeObject(data));
        }
    }
}
