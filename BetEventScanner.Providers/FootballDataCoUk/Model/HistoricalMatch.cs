namespace BetEventScanner.Common.Services.FootbalDataCoUk.Model
{
    public class HistoricalMatch
    {
        public string Div { get; set; }

        public string Date { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string FTHG { get; set; }

        public string FTAG { get; set; }

        public string FTR { get; set; }

        public string HTHG { get; set; }

        public string HTAG { get; set; }

        public string HTR { get; set; }

        // 1x2
        public string B365H { get; set; }

        public string B365D { get; set; }

        public string B365A { get; set; }

        // Total
        public string BbMxOver25 { get; set; }

        public string BbAvOver25 { get; set; }

        public string BbMxUnder25 { get; set; }

        public string BbAvUnder25 { get; set; }


        // HAndicap
        public string BbAHh { get; set; }

        public string BbMxAHH { get; set; }

        public string BbAvAHH { get; set; }

        public string BbMxAHA { get; set; }

        public string BbAvAHA { get; set; }
    }
}