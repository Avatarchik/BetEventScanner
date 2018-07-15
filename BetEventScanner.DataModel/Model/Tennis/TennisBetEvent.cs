namespace BetEventScanner.DataModel.Model.Tennis
{
    public class TennisBetEvent : TennisMatch
    {
        public string Player1Win { get; set; }

        public string Player1Handicap { get; set; }

        public string Player1HandicapOdds { get; set; }

        public string Player2Win { get; set; }

        public string Player2Handicap { get; set; }

        public string Player2HandicapOdds { get; set; }

        public string Player1ITotal { get; set; }

        public string Player1ITotalOverOdds { get; set; }

        public string Player1ITotalUnderOdds { get; set; }

        public string Player2ITotal { get; set; }

        public string Player2ITotalOverOdds { get; set; }

        public string Player2ITotalUnderOdds { get; set; }

        public string Total { get; set; }

        public string TotalOver { get; set; }

        public string TotalUnder { get; set; }
    }
}
