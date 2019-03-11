using BetEventScanner.Providers.FifaonlinecupOrg;
using BetEventScanner.Providers.Parimatch;
using BetEventScanner.Providers.Vprognoze;
using BetEventScanner.Providers.Vprognoze.Model;
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

            var h = File.ReadAllText(@"C:\BetEventScanner\tmp_1.html");
            var r = Converter.ToLiveBetMatches(h);
            var s = new Service();

            foreach (var item in r)
            {
                var h2h = s.GetHeadToHead(item.Player1.Name, item.Player2.Name);
            }

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
            var url = "https://www.parimatch.com/en/live.html";

            var events = new Dictionary<string, string>
            {
                //{"apl", "football11794772Item"},
                {"bundes1", "football11794770Item"}
            };

            while (true)
            {
                var htmls = HtmlParser.GetElementsByIds(url, events.Values.ToArray());
                var s = new Service();

                foreach (var h in htmls)
                {
                    var r1 = Converter.ToLiveBetMatches(h);

                    foreach (var item in r1)
                    {
                        var h2h = s.GetHeadToHead(item.Player1.Name, item.Player2.Name);
                    }

                    File.WriteAllText($@"C:\BetEventScanner\parimatch_live_1\{DateTime.Now.Ticks}.html", h);
                }

                Task.Delay(TimeSpan.FromSeconds(3)).GetAwaiter().GetResult();
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
