using System;

namespace BetEventScanner.DataModel.Model.Tennis
{
    public class TennisMatch
    {
        public string MatchId { get; set; }

        public DateTime DateTime { get; set; }

        public string Tournament { get; set; }

        public string Player1 { get; set; }

        public string Player2 { get; set; }

        public string Status { get; set; }
    }
}
