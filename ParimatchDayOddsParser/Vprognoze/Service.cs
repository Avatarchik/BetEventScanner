using BetEventScanner.Providers.Vprognoze;
using BetEventScanner.Providers.Vprognoze.Model;
using System.Collections.Generic;

namespace ParimatchDayOddsParser.Vprognoze
{
    class Service
    {
        private static void ParseVprognoze()
        {
            var html = ParimatchWebBrowser.ParseWebDriver("http://vprognoze.ru/statalluser/");
            var vpr = new VprProvider();
            var bettors = vpr.GetCurrentTopUsers(html);

            var d = new Dictionary<Bettor, Bet[]>();

            foreach (var bettor in bettors)
            {
                var link = vpr.GetBettorBetLink(bettor);
                var betsHtml = ParimatchWebBrowser.ParseWebDriver(link);
                var bettotBets = vpr.ParseBettorBets(bettor, betsHtml);

                d.Add(bettor, bettotBets);
            }
        }

        private static void ParseVprognozeIncomingAllBets()
        {
            var html = ParimatchWebBrowser.ParseWebDriver("https://vprognoze.ru/?do=searchbar&page=1");
            var vpr = new VprProvider();

            var incomingBetEvents = new Dictionary<Bettor, Bet[]>();

            var betEvents = vpr.ParseIncomingBets(html);
        }
    }
}
