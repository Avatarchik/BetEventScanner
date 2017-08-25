using BetEventScanner.DataAccess.Contracts;
using MongoDB.Bson;

namespace BetEventScanner.DataAccess.DataModel
{
    public class Tresult : IDocEntity
    {
        public Tresult(int matchId, int homeScored, int awayScored)
        {
            MatchId = matchId;
            HomeScored = homeScored;
            AwayScored = awayScored;
        }

        public int MatchId { get; }

        public int HomeScored { get; set; }

        public int AwayScored { get; set; }

        public ObjectId Id { get; set; }
    }
}