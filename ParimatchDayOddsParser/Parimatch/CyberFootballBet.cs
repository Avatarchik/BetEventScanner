using BetEventScanner.Providers.Parimatch.Models.CyberFootball;

namespace ParimatchDayOddsParser
{
    public class CyberFootballBet
    {
        public CyberFootballMatch Match { get; set; }
        public string EvNo => Match.EventNo;
        public string Bet { get; set; }
        public string BetKey { get; set; }
        public int Amount { get; set; }
        public string Odds { get; set; }
    }
}