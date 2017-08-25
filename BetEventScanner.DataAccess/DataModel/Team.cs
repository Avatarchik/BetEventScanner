using BetEventScanner.DataAccess.Contracts;
using MongoDB.Bson;

namespace BetEventScanner.DataAccess.DataModel
{
    public class Team : IDocEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string ShortName { get; set; }

        public ObjectId Id { get; set; }
    }
}
