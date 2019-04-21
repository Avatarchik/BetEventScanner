using BetEventScanner.Providers.Parimatch.Models.CyberFootball;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ParimatchDayOddsParser.StatsTesting
{
    public class CompetitionStats
    {
        public string Competition { get; set; }

        public Dictionary<string, List<CyberFootballMatch>> m = new Dictionary<string, List<CyberFootballMatch>>();
    }

    public static class OppositeSerie
    {
        public static void Test()
        {
            var d = new Dictionary<string, CompetitionStats>();

            var matches = JsonConvert.DeserializeObject<CyberFootballMatch[]>(File.ReadAllText($@"C:\BetEventScanner\cyberFootball\results\cyberMatches.json"));
            matches.GroupBy(x=>x.Competition).ToList().ForEach(x=> 
            {
                var s = new CompetitionStats();
                s.Competition = x.Key;
                d[x.Key] = s;

                foreach (var item in x)
                {

                }
            });
        }
    }
}
