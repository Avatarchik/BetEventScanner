using System;
using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataModel;
using MongoDB.Bson;

namespace BetEventScanner.Providers.Parimatch.Model
{
    public class ParimatchTennisBetEvent : IParimatchEvent, IDocEntity
    {
        public string Evno { get; set; }

        public SportType EventType { get; set; }

        public string Header { get; set; }

        public ObjectId Id { get; set; }

        public string Tournament { get; set; }
        public DateTime DateTime { get; internal set; }
        public string Player1 { get; internal set; }
        public string Player2 { get; internal set; }
        public string Player1Handicap { get; internal set; }
        public string Player2Handicap { get; internal set; }
        public string Player1HandicapOdds { get; internal set; }
        public string Player2HandicapOdds { get; internal set; }
        public string Total { get; internal set; }
        public string TotalOver { get; internal set; }
        public string TotalUnder { get; internal set; }
        public string Player1Win { get; internal set; }
        public string Player2Win { get; internal set; }
        public string TwoZero { get; internal set; }
        public object TwoOne { get; internal set; }
        public string OneTwo { get; internal set; }
        public string Player1ITotal { get; internal set; }
        public string Player2ITotal { get; internal set; }
        public string Player1ITotalOverOdds { get; internal set; }
        public string Player2ITotalOverOdds { get; internal set; }
        public string Player1ITotalUnderOdds { get; internal set; }
        public string Player2ITotalUnderOdds { get; internal set; }
        public string MatchId { get; internal set; }
        public string Status { get; internal set; }
        public string ZeroTwo { get; internal set; }
    }
}