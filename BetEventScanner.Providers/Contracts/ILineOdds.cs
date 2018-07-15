using BetEventScanner.Providers.Parimatch.Model;
using System.Collections.Generic;

namespace BetEventScanner.Providers.Contracts
{
    public interface IOddsParser
    {
        ICollection<IParimatchEvent> GetFutureOddsBetEvents();

        ICollection<IParimatchEvent> GetFutureOddsBetEvents(string sourceHtml);
    }
}
