using BetEventScanner.Providers.Parimatch.Model;
using System.Collections.Generic;

namespace BetEventScanner.Providers.Contracts
{
    public interface IOddsProvider
    {
        ICollection<IParimatchEvent> GetFutureOddsBetEvents();

        ICollection<IParimatchEvent> ParsePreMatchOdds(string sourceHtml);
    }
}
