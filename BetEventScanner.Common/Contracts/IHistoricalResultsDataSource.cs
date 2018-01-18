using System.Collections.Generic;
using BetEventScanner.DataModel.Model;

namespace BetEventScanner.Common.Contracts
{
    public interface IHistoricalResultsDataSource
    {
        ICollection<FootballMatchResult> GetHistoricalMatches(string filePath);
    }
}