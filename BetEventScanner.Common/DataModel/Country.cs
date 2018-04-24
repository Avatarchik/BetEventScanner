using BetEventScanner.DataAccess.Contracts;
using MongoDB.Bson;

namespace BetEventScanner.Common.DataModel
{
    public class Country : IDocEntity, IEntity
    {
        public ObjectId Id { get; set; }

        public int SqlId { get; set; }

        public string Name { get; set; }
    }
}
