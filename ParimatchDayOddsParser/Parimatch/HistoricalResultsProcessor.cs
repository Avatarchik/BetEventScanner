using BetEventScanner.Providers.Parimatch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParimatchDayOddsParser.Parimatch
{
    public class HistoricalResultsProcessor : IProcessor
    {
        public void Process()
        {
            var resultMatches = new List<HistoricalMatchResult>();
            
            for (int i = 0; i < 100; i++)
            {
                var dt = DateTime.Now.AddDays(-i);
                var html = GetHtml(dt);
                var matches = HistoricalResultsParser.GetMatches(html);
                var filtered = matches.Where(x => x.Competition.ToLower().Contains("esports battle"));
                resultMatches.AddRange(filtered);
            }
        }

        private string GetHtml(DateTime dt) =>        
            ParimatchWebBrowser.GetPageHtml(HistoricalResultsParser.FootballUrl(DateTime.Now));
    }
}
