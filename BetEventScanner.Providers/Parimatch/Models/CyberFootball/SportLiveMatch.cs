using BetEventScanner.DataModel;
using System;

namespace BetEventScanner.Providers.Parimatch.Models.CyberFootball
{
    public class SportLiveMatch
    {
        public string ErrorName { get; set; }
        public string ErrorText { get; set; }
        public string EventNo { get; set; }
        public string Link { get; set; }
        public string SportTypeStr { get; set; }
        public SportType SportType { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string Result { get; set; }
        public string TotalOverBetKey { get; set; }
        public string TotalUnderOdds { get; set; }
        public string TotalUnderBetKey { get; set; }
        public string DrawOdds { get; set; }
        public string Fora1betKey { get; set; }
        public string TotalOverOdds { get; set; }
        public string TotalValue { get; set; }
        public string Fora2Odds { get; set; }
        public string Fora2betKey { get; set; }
        public string Fora2Value { get; set; }
        public string Fora1Odds { get; set; }
        public string Fora1Value { get; set; }
        public string Win2Odds { get; set; }
        public string Win2betKey { get; set; }
        public string DrawbetKey { get; set; }
        public string Win1Odds { get; set; }
        public string Win1betKey { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Competition { get; set; }

        public static string Key(CyberFootballLiveMatch lbm) =>
            $"{lbm.EventNo}{lbm.Player1.Team}{lbm.Player1.Name}{lbm.Player2.Team}{lbm.Player1.Name}";

        public static SportLiveMatch Error(string en, string et) => new SportLiveMatch
        {
            ErrorName = en,
            ErrorText = et
        };
    }
}
