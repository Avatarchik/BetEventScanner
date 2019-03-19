using System;
using System.Linq;
using BetEventScanner.Providers.Parimatch;

namespace ParimatchDayOddsParser
{
    public class ParimatchLiveBetProcessor
    {
        private readonly LiveParser _liveParser;

        public ParimatchLiveBetProcessor()
        {
            _liveParser = new LiveParser();
        }

        public void Process()
        {
            var container = ParimatchWebBrowser.GetElementByCssSelector(LiveParser.LiveUrl, "div.container > div.wrapper");
            var liveEvents = _liveParser.GetListLiveEvents(container);
            Console.Clear();
            Console.WriteLine($"Live matches: {liveEvents.Length} / failed: {liveEvents.Count(x => !string.IsNullOrEmpty(x.ErrorName))}");
            var nle = liveEvents.Where(x => string.IsNullOrEmpty(x.ErrorName)).ToArray();
            foreach (var item in nle)
            {
                Console.WriteLine($"{item.SportType}: {item.Team1} - {item.Team2}, w={item.Win1Odds}; x={item.DrawOdds} l={item.Win2Odds} r={item.Result}");
            }
        }
    }
}
