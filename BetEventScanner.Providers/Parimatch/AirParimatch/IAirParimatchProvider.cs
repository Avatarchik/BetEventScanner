using System.Collections.Generic;
using BetEventScanner.Providers.Parimatch.Model;

namespace BetEventScanner.Providers.Parimatch
{
    public interface IAirParimatchProvider
    {
        ICollection<IParimatchEvent> GetFutureOddsBetEvents();
        ICollection<IParimatchEvent> ParsePreMatchOdds(string sourceHtml);
    }
}