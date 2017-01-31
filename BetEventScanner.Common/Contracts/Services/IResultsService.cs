using System.Collections.Generic;

namespace BetEventScanner.Common.Contracts.Services
{
    public interface IResultsService
    {
        IEnumerable<IMatchResult> GetResults(string filePath);
    }
}
