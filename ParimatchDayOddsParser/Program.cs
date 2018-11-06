using BetEventScanner.Providers.Parimatch;
using BetEventScanner.Providers.Vprognoze;
using BetEventScanner.Providers.Vprognoze.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParimatchDayOddsParser
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            while (true)
            {
                try
                {
                    //ParseParimatch();
                    //ParseVprognoze();
                    ParseVprognozeIncomingAllBets();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }

        private static void ParseParimatch()
        {
            var html = HtmlParser.ParseWebDriverFromAttribute("https://air.parimatch.com/en/prematch/0-24/", "prematch-sports");
            var parimatch = new AirParimatchProvider();

            var prematchEvents = parimatch.ParsePreMatchOdds(html);
        }

        private static void ParseVprognoze()
        {
            var html = HtmlParser.ParseWebDriver("http://vprognoze.ru/statalluser/");
            var vpr = new VprProvider();
            var bettors = vpr.GetCurrentTopUsers(html);

            var d = new Dictionary<Bettor, Bet[]>();

            foreach (var bettor in bettors)
            {
                var link = vpr.GetBettorBetLink(bettor);
                var betsHtml = HtmlParser.ParseWebDriver(link);
                var bettotBets = vpr.ParseBettorBets(bettor, betsHtml);

                d.Add(bettor, bettotBets);
            }
        }

        private static void ParseVprognozeIncomingAllBets()
        {
            var html = HtmlParser.ParseWebDriver("https://vprognoze.ru/?do=searchbar&page=1");
            var vpr = new VprProvider();
           
            var incomingBetEvents = new Dictionary<Bettor, Bet[]>();

            var betEvents = vpr.ParseIncomingBets(html);
        }
    }
}
