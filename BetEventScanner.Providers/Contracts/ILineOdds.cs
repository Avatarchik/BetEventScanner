using BetEventScanner.Providers.Parimatch.Model;
using System.Collections.Generic;

namespace BetEventScanner.Providers.Contracts
{
    public interface IOddsProvider
    {
        ICollection<IParimatchEvent> GetTodayBetEvents();

        ICollection<IParimatchEvent> ParsePreMatchOdds(string sourceHtml);
    }
}
