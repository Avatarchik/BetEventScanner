using BetEventScanner.DataAccess.Contracts;
using MongoDB.Bson;

namespace BetEventScanner.Common.DataModel
{
    public class Country : IDocEntity
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }
    }
}
