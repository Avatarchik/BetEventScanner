using System.IO;
using Newtonsoft.Json;
using SoccerStandParser;

namespace BetEventScanner.SoccerstandScaner
{
    public class SoccerStandParserStorage : IParserStorage<SoccerstandData>
    {
        public SoccerstandData LoadOriginSource(string url)
        {
            return JsonConvert.DeserializeObject<SoccerstandData>(File.ReadAllText($@"C:\BetEventScanner\soccer_stand\{url.Replace("\\", "")}.json"));
        }

        public void Store(SoccerstandData data)
        {
            var name = StringHelper.RemoveSpecialSymbols(data.Url);

            File.WriteAllText($@"C:\BetEventScanner\soccer_stand\{name}.json", JsonConvert.SerializeObject(data));
        }
    }
}