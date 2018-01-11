using System.Collections.Generic;
using System.IO;
using BetEventScanner.Providers.Contracts;
using BetEventScanner.Providers.SoccerStandCom.Model;
using Newtonsoft.Json;

namespace BetEventScanner.Providers.SoccerStandCom
{
    public class SoccerStandDataSource : IDataSource<SoccerstandData>
    {
        private readonly ParseSettings _settings;

        public SoccerStandDataSource(ParseSettings settings)
        {
            _settings = settings;
        }

        public ICollection<SoccerstandData> GetSourceData()
        {
            var res = new List<SoccerstandData>();

            foreach (var file in new DirectoryInfo("C:\\BetEventScanner\\soccer_stand").GetFiles())
            {
                res.Add(JsonConvert.DeserializeObject<SoccerstandData>(File.ReadAllText(file.FullName))); 
            }

            return res;
        }
    }
}