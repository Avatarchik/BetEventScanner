using System.Collections.Generic;

namespace BetEventScanner.Common.Contracts
{
    public interface IResultsService
    {
        IEnumerable<IMatchResult> GetResults(string filePath);
    }
}
