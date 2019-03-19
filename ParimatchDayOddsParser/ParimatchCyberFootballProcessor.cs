using BetEventScanner.Providers.FifaonlinecupOrg;
using BetEventScanner.Providers.Parimatch;
using BetEventScanner.Providers.Parimatch.Models.CyberFootball;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ParimatchDayOddsParser
{
    public class ParimatchCyberFootballProcessor
    {
        private readonly IHeadToHeadProvider _headToHeadProvider;

        public ParimatchCyberFootballProcessor(IHeadToHeadProvider headToHeadProvider)
        {
            _headToHeadProvider = headToHeadProvider;
        }

        public void Process()
        {
            var htmls = ParimatchWebBrowser.GetElementsByIds(LiveParser.LiveUrl, ParimatchCyberFootball.Items.Values.ToArray());

            var pmLiveService = new LiveParser();
            var cache = new Dictionary<string, string>();
            
            foreach (var lbe in pmLiveService.ConvertToLiveBetEvets(htmls.Select(x => x.Html).ToArray()))
            {
                if (!Directory.Exists($@"C:\BetEventScanner\parimatch_live\{lbe.EventNo}"))
                    Directory.CreateDirectory($@"C:\BetEventScanner\parimatch_live\{lbe.EventNo}");

                File.WriteAllText($@"C:\BetEventScanner\parimatch_live\{lbe.EventNo}\{DateTime.Now.Ticks}.html", JsonConvert.SerializeObject(lbe));

                var key = CyberFootballLiveMatch.Key(lbe);
                if (cache.ContainsKey(key))
                    continue;

                cache.Add(key, key);

                var h2h = _headToHeadProvider.GetHeadToHead(lbe.Player1.Name, lbe.Player2.Name);
                var h2hStatistics = HeadToHeadCalculator.CalculateHead2Head(h2h);
                Console.WriteLine($"Match:({lbe.Player1.Name}/{lbe.Player1.Team}) - ({lbe.Player2.Name}/{lbe.Player2.Team})");
                Console.WriteLine($"P1:{h2hStatistics.P1WinsCount} - D:{h2hStatistics.DrawsCount} - P2:{h2hStatistics.P2WinsCount}");
            }
        }
    }
}
