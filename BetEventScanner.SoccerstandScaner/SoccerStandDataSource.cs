using System.IO;
using BetEventScanner.SoccerstandScaner.Contracts;
using Newtonsoft.Json;
using SoccerStandParser;

namespace BetEventScanner.SoccerstandScaner
{
    public class SoccerStandDataSource : IDataSource
    {
        private readonly ParseSettings _settings;

        public SoccerStandDataSource(ParseSettings settings)
        {
            _settings = settings;
        }

        public SoccerstandData GetSourceData()
        {
            var name = StringHelper.RemoveSpecialSymbols(_settings.Url);
            return JsonConvert.DeserializeObject<SoccerstandData>(File.ReadAllText($@"C:\BetEventScanner\soccer_stand\{name}.json"));
        }
    }
}