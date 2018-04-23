using System.Collections.Generic;
using BetEventScanner.Common.Services.Csv;
using BetEventScanner.Common.Services.FootbalDataCoUk.Mappings;
using BetEventScanner.Common.Services.FootbalDataCoUk.Model;
using BetEventScanner.Providers.FootballDataCoUk.Mappings;
using CsvHelper.Configuration;
using Newtonsoft.Json.Linq;

namespace BetEventScanner.Providers.FootballDataCoUk
{
    public class FootballDataCoUkParser : CsvParserBase
    {
        public ICollection<HistoricalMatch> GetHistoricalResults(string filePath)
        {
            //return Parse<HistoricalMatch>(filePath, new FootballDataCoUkHistoricalMapping());
            return null;
        }

        public ICollection<FixtureMatch> GetFixture(string filePath)
        {
            //return Parse<FixtureMatch>(filePath, new FootballDataCoUkFixtureMapping());
            return null;
        }

        public ICollection<JObject> GetDynamicHistoricalResults(string filePath, ICollection<string> headers)
        {
            return Parse(filePath, headers);
        }

        public ICollection<string> GetFileHeaders(string filePath)
        {
            return base.GetHeaders(filePath);
        }
    }
}
