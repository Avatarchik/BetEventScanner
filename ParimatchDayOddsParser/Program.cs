using BetEventScanner.Providers.FifaonlinecupOrg;
using BetEventScanner.Providers.Parimatch;
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
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var parimatchCyberFootball = new ParimatchCyberFootballProcessor(new Service());
            var pmLive = new ParimatchLiveBetProcessor();
            //var pmCreds = JsonConvert.DeserializeObject<ParimatchCredentials>(File.ReadAllText(@"C:\BetEventScanner\Parimatch\creds.json"));
            //ParimatchWebBrowser.Login(pmCreds);
            //ParimatchWebBrowser.PlaceBet("39816954");
            // TODO Complete creating stats table
            //var html = ParimatchApiClient.DownloadHtmlWC("https://s5.sir.sportradar.com/parimatch/en/1/season/55737");
            //var table = new StatisticsParser().GetTableStats(html);
            var cyberLive = false;
            var generalLive = true;

            while (true)
            {
                try
                {
                    //ParseParimatch();
                    //ParseVprognoze();
                    //ParseVprognozeIncomingAllBets();
                    if (cyberLive)
                        parimatchCyberFootball.Process();

                    if (generalLive)
                        pmLive.Process();


                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                await Task.Delay(TimeSpan.FromSeconds(30));
            }
        }



        private static void ParseParimatch()
        {
            var html = ParimatchWebBrowser.GetElementByTagName("https://air.parimatch.com/en/prematch/0-24/", "prematch-sports");
            var parimatch = new AirParimatchProvider();

            var prematchEvents = parimatch.ParsePreMatchOdds(html);
        }

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
