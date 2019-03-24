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
            var html = ParimatchWebBrowser.GetPageHtml("https://www.parimatch.com/en/bet.html?ha=20181213");
            var dateRes = new OldParimatchProvider(new ParimatchSettings()).ParseHistoricalResults(html);

            return;

            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var parimatchCyberFootball = new ParimatchCyberFootballProcessor(new Service());
            var pmLive = new ParimatchLiveBetProcessor();
            var pmHistorical = new ParimatchHistoricalResultsProcessor();
            //var pmCreds = JsonConvert.DeserializeObject<ParimatchCredentials>(File.ReadAllText(@"C:\BetEventScanner\Parimatch\creds.json"));
            //ParimatchWebBrowser.Login(pmCreds);
            //ParimatchWebBrowser.PlaceBet("39816954");
            // TODO Complete creating stats table
            //var html = ParimatchApiClient.DownloadHtmlWC("https://s5.sir.sportradar.com/parimatch/en/1/season/55737");
            //var table = new StatisticsParser().GetTableStats(html);
            var cyberLive = false;
            var generalLive = false;

            while (true)
            {
                try
                {
                    Console.Clear();
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
