using BetEventScanner.DataAccess.Contracts;
using MongoDB.Bson;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class GeneralTableDocEntity : IDocEntity
    {
        public int Teams { get; set; } = 2;

        public int Competitions { get; set; } = 3;

        public int Fixtures { get; set; } = 4;

        public int Statistics { get; set; } = 5;
        public ObjectId Id { get; set; }
    }
}