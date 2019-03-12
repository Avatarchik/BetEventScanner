using BetEventScanner.Providers.FifaonlinecupOrg;
using BetEventScanner.Providers.Parimatch;
using BetEventScanner.Providers.Parimatch.Models.CyberFootball;
using BetEventScanner.Providers.Vprognoze;
using BetEventScanner.Providers.Vprognoze.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ParimatchDayOddsParser
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //new AccountHistoryParser().ParseFromFile(@"C:\BetEventScanner\account_history_parimatch.html");

            var rs = new Service().GetResults().Length;

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
                    //ParseVprognozeIncomingAllBets();

                    ParseParimatchLive();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }

        private static void ParseParimatchLive()
        {
            var pmLiveService = new LiveService();
            var cache = new Dictionary<string, string>();

            while (true)
            {
                var htmls = HtmlParser.GetElementsByIds(pmLiveService.GetLiveUrl, pmLiveService.GetEventItems());
                var csl = new Service();

                foreach (var lbe in pmLiveService.ConvertToLiveBetEvets(htmls))
                {
                    if (!Directory.Exists($@"C:\BetEventScanner\parimatch_live\{lbe.EventNo}"))
                        Directory.CreateDirectory($@"C:\BetEventScanner\parimatch_live\{lbe.EventNo}");

                    File.WriteAllText($@"C:\BetEventScanner\parimatch_live\{lbe.EventNo}\{DateTime.Now.Ticks}.html", JsonConvert.SerializeObject(lbe));

                    var key = LiveBetMatch.Key(lbe);
                    if (cache.ContainsKey(key))
                        continue;

                    cache.Add(key, key);

                    var h2h = csl.GetHeadToHead(lbe.Player1.Name, lbe.Player2.Name);
                    var h2hStatistics = csl.CalculateHead2Head(h2h);
                    Console.WriteLine($"Match:({lbe.Player1.Name}/{lbe.Player1.Team}) - ({lbe.Player2.Name}/{lbe.Player2.Team})");
                    Console.WriteLine($"P1:{h2hStatistics.P1WinsCount} - D:{h2hStatistics.DrawsCount} - P2:{h2hStatistics.P2WinsCount}");
                }

                Task.Delay(TimeSpan.FromSeconds(30)).GetAwaiter().GetResult();
            }
        }

        private static void ParseParimatch()
        {
            var html = HtmlParser.GetElementByTagName("https://air.parimatch.com/en/prematch/0-24/", "prematch-sports");
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

        private static void ParseFifaonlinecupOrg()
        {

        }
    }
}
