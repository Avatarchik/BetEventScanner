using System;
using System.Collections.Generic;

namespace BetEventScanner.Common.DataModel
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime MatchDateTime { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
    }
}
