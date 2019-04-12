using BetEventScanner.Providers.Parimatch;
using BetEventScanner.Providers.Strategies.Basketball;
using System;
using System.Linq;

namespace ParimatchDayOddsParser.Parimatch
{
    public class Service
    {
        public static void HistoricalHumptyDumptyTester()
        {
            var html = ParimatchWebBrowser.GetPageHtml("https://www.parimatch.com/en/bet.html?ha=20181213");
            var hd = new HumptyDumpty();
            var dateRes = new OldParimatchProvider(new ParimatchSettings()).ParseHistoricalResults(html);
            Console.WriteLine($"parsed:{dateRes.Length}, errors:{dateRes.Count(x => x.Error)}");
            hd.Process(dateRes.Where(x => !x.Error && !x.Result.Contains("0:0") && x.SportType == BetEventScanner.DataModel.SportType.Basketball && decimal.Parse(x.Win1Odds) >= 1.1m && decimal.Parse(x.Win2Odds) >= 1.1m).ToArray());
            var r = hd.GetResult();
            Console.WriteLine($"t:{r.Total}, allPeriodsWinOneTeam:{r.AllPeriodsWinOneTeamCount}");

            foreach (var i in r.Matches)
            {
                Console.WriteLine($"w1={i.Win1Odds}; w2={i.Win2Odds}; r={i.Result}");
            }

            /*
             //var pmCreds = JsonConvert.DeserializeObject<ParimatchCredentials>(File.ReadAllText(@"C:\BetEventScanner\Parimatch\creds.json"));
            //ParimatchWebBrowser.Login(pmCreds);
            //ParimatchWebBrowser.PlaceBet("39816954");
            // TODO Complete creating stats table
            //var html = ParimatchApiClient.DownloadHtmlWC("https://s5.sir.sportradar.com/parimatch/en/1/season/55737");
            //var table = new StatisticsParser().GetTableStats(html);
             */
        }
    }
}
