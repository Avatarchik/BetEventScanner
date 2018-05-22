using System;
using BetEventScanner.DataAccess.Contracts;
using MongoDB.Bson;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class FixtureDocEntity : IDocEntity
    {
        public DateTime Date { get; set; }

        public string Status { get; set; }

        public string Matchday { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public MatchResultEntity MatchResult { get; set; }

        public OddsEntity Odds { get; set; }
        public ObjectId Id { get; set; }
    }
}