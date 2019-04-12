using BetEventScanner.Providers.FifaonlinecupOrg;
using BetEventScanner.Providers.Parimatch;
using BetEventScanner.Providers.Parimatch.Models.CyberFootball;
using System;
using System.Collections.Generic;
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
            //var html = ParimatchWebBrowser.GetPageHtml(ParimatchConverter.LiveUrl);
            //var cyberFootballSection = GetCyberFootballSection(html);

            var container = ParimatchWebBrowser.GetElementByCssSelector(ParimatchConverter.LiveUrl, "div.container > div.wrapper");
            var liveEvents = ParimatchConverter.GetListLiveEvents2(container);
            

            var cache = new Dictionary<string, string>();

            foreach (var lbe in ParimatchConverter.ConvertToLiveBetEvets(""))
            {
                var key = CyberFootballLiveMatch.Key(lbe);
                if (cache.ContainsKey(key))
                    continue;

                cache.Add(key, key);

                var h2h = _headToHeadProvider.GetHeadToHead(lbe.Player1.Name, lbe.Player2.Name);
                var h2hStatistics = CalculateHead2Head(h2h);
                Console.WriteLine($"Match:({lbe.Player1.Name}/{lbe.Player1.Team}) - ({lbe.Player2.Name}/{lbe.Player2.Team})");
                Console.WriteLine($"P1:{h2hStatistics.P1WinsCount} - D:{h2hStatistics.DrawsCount} - P2:{h2hStatistics.P2WinsCount}");
            }
        }

        private static HeadToHeadStats CalculateHead2Head(HeadToHead h2h, int take = 20)
        {
            var r = new HeadToHeadStats
            {
                Player1 = h2h.Player1,
                Player2 = h2h.Player2
            };

            if (h2h.Results == null)
                return r;

            foreach (var q in h2h.Results.Where(v => v.Status != "cancelled").OrderByDescending(x => x.Date).Take(take))
            {
                var p1 = q.Player1.Name;
                var p2 = q.Player2.Name;
                var result = q.FT.Split(':').Select(int.Parse).ToList();

                if (result[0] == result[1])
                {
                    r.DrawsCount++;
                    continue;
                }

                if (result[0] > result[1])
                {
                    if (p1 == r.Player1)
                    {
                        r.P1WinsCount++;
                    }
                    else
                    {
                        r.P2WinsCount++;
                    }
                    continue;
                }

                if (result[0] < result[1])
                {
                    if (p2 == r.Player2)
                    {
                        r.P2WinsCount++;
                    }
                    else
                    {
                        r.P1WinsCount++;
                    }
                }
            }

            return r;
        }
    }
}
