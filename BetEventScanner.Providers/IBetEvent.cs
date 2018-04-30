using System.Collections.Generic;

namespace BetEventScanner.Providers
{
    public interface IFootballBetEvent
    {
        Dictionary<string, decimal> Odds { get; set; }
    }
}