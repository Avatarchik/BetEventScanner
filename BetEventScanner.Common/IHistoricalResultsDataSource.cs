using System.Collections.Generic;
using BetEventScanner.DataModel;

namespace BetEventScanner.Common
{
    public interface IHistoricalResultsDataSource
    {
        ICollection<FootballMatchResult> GetHistoricalMatches(string filePath);
    }
}