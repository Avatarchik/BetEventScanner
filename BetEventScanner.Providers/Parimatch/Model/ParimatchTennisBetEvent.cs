using System;
using System.Collections.Generic;

namespace BetEventScanner.Providers.Parimatch.Model
{
    public class ParimatchTennisBetEvent : IParimatchEvent
    {
        public string MatchId { get; set; }

        public string ParimatchId { get; set; }

        public string Type { get; set; }

        public DateTime DateTime { get; set; }

        public string Player1 { get; set; }

        public string Player2 { get; set; }

        public string Player1Handicap { get; set; }

        public string Player1HandicapOdds { get; set; }

        public string Player2Handicap { get; set; }

        public string Player2HandicapOdds { get; set; }

        public string Player1Win { get; set; }

        public string Player2Win { get; set; }

        public string TwoZero { get; set; }

        public string TwoOne { get; set; }

        public string OneTwo { get; set; }

        public string ZeroTwo { get; set; }

        public string Player1ITotal { get; set; }

        public string Player1ITotalOverOdds { get; set; }

        public string Player1ITotalUnderOdds { get; set; }

        public string Player2ITotal { get; set; }

        public string Player2ITotalOverOdds { get; set; }

        public string Player2ITotalUnderOdds { get; set; }

        public string Total { get; set; }

        public string TotalOver { get; set; }

        public string TotalUnder { get; set; }

        public string FinalScore { get; set; }

        public ICollection<string> SetsResult { get; set; }
        public string Status { get; internal set; }
    }
}