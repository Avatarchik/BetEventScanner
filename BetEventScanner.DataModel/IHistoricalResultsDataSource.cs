using System.Collections.Generic;

namespace BetEventScanner.DataModel
{
    public interface IHistoricalResultsDataSource
    {
        ICollection<FootballResult> GetHistoricalMatches(string filePath);
    }
}