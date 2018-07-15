using System.Collections.Generic;
using System.Linq;
using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataAccess.Mongo;

namespace BetEventScanner.Providers.Parimatch
{
    public class FutureOddsBetEventsStorage
    {
        public void Store<T>(IEnumerable<T> incomingItems) where T : class, IOddsBetEvent, IDocEntity
        {
            var repo = new MongoRepository<T>(typeof(T).Name);
            repo.InsertEntities(incomingItems);
        }

        public ICollection<T> Get<T>() where T : class, IOddsBetEvent, IDocEntity
        {
            var repo = new MongoRepository<T>(typeof(T).Name);
            return repo.GetAll().ToList();
        }
    }

}
