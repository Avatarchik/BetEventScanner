using System.Collections.Generic;
using BetEventScanner.Common.Services.FootbalDataCoUk.Mappings;
using BetEventScanner.Common.Services.FootbalDataCoUk.Model;

namespace BetEventScanner.Common.Services.FootbalDataCoUk
{
    public class FootballDataCoUkParser : CsvParserBase
    {
        public ICollection<HistoricalMatch> GetHistoricalResults(string filePath)
        {
            return Parse<HistoricalMatch>(filePath, new FootballDataCoUkHistoricalMapping());
        }

        public ICollection<FixtureMatch> GetFixture(string filePath)
        {
            return Parse<FixtureMatch>(filePath, new FootballDataCoUkFixtureMapping());
        }
    }
}
