using BetEventScanner.Providers.Parimatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ParimatchDayOddsParser.Parimatch
{
    public class HistoricalResultsProcessor : IProcessor
    {
        public void Process()
        {
            var resultMatches = new List<CyberFootballHistoricalMatchResult>();
            
            for (int i = 0; i < 365; i++)
            {
                var dt = DateTime.Now.AddDays(-i);
                var html = GetHtml(dt);
                var matches = HistoricalResultsParser.GetMatches(html);
                var filtered = matches.Where(x => x.Competition.ToLower().Contains("cyberfootball")).ToArray();
                resultMatches.AddRange(filtered);

                Thread.Sleep(1000);
            }

            System.IO.File.WriteAllText($@"C:\BetEventScanner\cyberFootball\results\index.json", JsonConvert.SerializeObject(resultMatches));
        }

        private string GetHtml(DateTime dt) =>        
            ParimatchWebBrowser.GetPageHtml(HistoricalResultsParser.FootballUrl(dt));
    }
}
