using System.Collections.Generic;
using BetEventScanner.DataAccess.Contracts;
using MongoDB.Bson;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class Tmatch : IDocEntity
    {
        public Tmatch(int matchId, string team1, string team2)
        {
            MatchId = matchId;
            Team1 = team1;
            Team2 = team2;
          
        }

        public int MatchId { get; set; }

        public string Team1 { get; set; }

        public string Team2 { get; set; }

        public ObjectId Id { get; set; }

        public ICollection<Tbet> Bets { get; set; } = new List<Tbet>();

        public void AddBet(Tbet bet)
        {
            Bets.Add(bet);
        }
    }
}