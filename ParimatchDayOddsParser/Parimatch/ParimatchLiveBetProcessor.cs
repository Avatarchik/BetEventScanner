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
            var liveEvents = ParimatchConverter.GetListLiveEvents(container);
            Console.Clear();
            Console.WriteLine($"Live matches: {liveEvents.Length} / failed: {liveEvents.Count(x => !string.IsNullOrEmpty(x.ErrorName))}");
            var nle = liveEvents.Where(x => string.IsNullOrEmpty(x.ErrorName)).ToArray();

            var basketball = ParimatchConverter.GetBasketballLiveResults(nle);

            foreach (var item in nle)
            {
                Console.WriteLine($"{item.SportTypeStr}: {item.Team1} - {item.Team2} {item.Result}");
                Console.WriteLine($"w={item.Win1Odds}; x={item.DrawOdds} l={item.Win2Odds} r={item.Result}");
                Console.WriteLine($"f1={item.Fora1Value}:{item.Fora1Odds}; f2={item.Fora2Value}:{item.Fora2Odds}; t={item.TotalValue} o={item.TotalOverOdds} u={item.TotalUnderOdds}");
            }
        }
    }
}
