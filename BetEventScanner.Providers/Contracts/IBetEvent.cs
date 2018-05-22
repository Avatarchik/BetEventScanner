using System.Collections.Generic;

namespace BetEventScanner.Providers.Contracts
{
    public interface IFootballBetEvent
    {
        Dictionary<string, decimal> Odds { get; set; }
    }
}