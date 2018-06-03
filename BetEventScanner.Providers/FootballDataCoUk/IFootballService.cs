using System.Collections.Generic;

namespace BetEventScanner.Providers.FootballDataCoUk
{
    public interface IFootballService
    {
        SourceProvider Provider { get; }

        IEnumerable<FootballMatchResult> GetAllResults();
    }
}