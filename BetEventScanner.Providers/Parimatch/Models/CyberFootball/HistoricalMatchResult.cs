using System;

namespace BetEventScanner.Providers.Parimatch.Models.CyberFootball
{
    public class HistoricalMatchResult
    {
        public bool Error { get; set; }
        public SportType SportType { get; set; }
        public string EventNo { get; set; }
        public DateTime DateTime { get; set; }
        public string SportTypeStr { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string Result { get; set; }
        public string TotalUnderOdds { get; set; }
        public string DrawOdds { get; set; }
        public string TotalOverOdds { get; set; }
        public string TotalValue { get; set; }
        public string Fora2Odds { get; set; }
        public string Fora2Value { get; set; }
        public string Fora1Odds { get; set; }
        public string Fora1Value { get; set; }
        public string Win2Odds { get; set; }
        public string Win1Odds { get; set; }
        public string Ind1TotalValue { get; set; }
        public string Ind2TotalValue { get; set; }
        public string Ind1TotalUpper { get; set; }
        public string Ind2TotalUpper { get; set; }
        public string Ind1TotalUnder { get; set; }
        public string Ind2TotalUnder { get; set; }
    }
}
