using System;
using System.Linq;
using BetEventScanner.Providers.Parimatch;

namespace ParimatchDayOddsParser
{
    public class ParimatchLiveBetProcessor
    {
        public void Process()
        {
            var container = ParimatchWebBrowser.GetElementByCssSelector(ParimatchConverter.LiveUrl, "div.container > div.wrapper");
            var liveEvents = ParimatchConverter.GetListLiveEvents2(container);
            Console.Clear();
            Console.WriteLine($"Live matches: {liveEvents.Length}");
            var basketball = ParimatchConverter.GetBasketballLiveResults(liveEvents);

            foreach (var item in liveEvents)
            {
                Console.WriteLine($"{item.SportType}: {item.Team1} - {item.Team2} {item.Result}");
                Console.WriteLine($"w={item.Win1Odds}; x={item.DrawOdds} l={item.Win2Odds} r={item.Result}");
                Console.WriteLine($"f1={item.Fora1Value}:{item.Fora1Odds}; f2={item.Fora2Value}:{item.Fora2Odds}; t={item.TotalValue} o={item.TotalOverOdds} u={item.TotalUnderOdds}");
            }
        }
    }
}
