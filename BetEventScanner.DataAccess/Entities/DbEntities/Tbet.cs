using System;
using BetEventScanner.DataAccess.Contracts;
using MongoDB.Bson;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class Tbet : IDocEntity
    {
        public Tbet(int matchId, string bet, decimal value, decimal odds)
        {
            MatchId = matchId;
            Bet = bet;
            Value = value;
            Odds = odds;
        }

        public int MatchId { get; set; }

        public string Bet { get; set; }

        public decimal Value { get; set; }

        public decimal Odds { get; set; }

        public ObjectId Id { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}