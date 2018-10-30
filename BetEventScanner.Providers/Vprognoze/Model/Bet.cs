using System;

namespace BetEventScanner.Providers.Vprognoze.Model
{
    public enum BetResult
    {
        None,
        Win,
        Lost,
        Draw
    }

    public class Bet
    {
        public double Odds { get; set; }
        public DateTime DateTime { get; set; }
        public string Competition { get; set; }
        public string BetEvent { get; set; }
        public string BetVar { get; set; }
        public string EventResult { get; set; }
        public BetResult BetResult { get; set; } = BetResult.None;
    }
}
