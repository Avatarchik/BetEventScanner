using BetEventScanner.Providers.FifaonlinecupOrg;
using BetEventScanner.Providers.Parimatch;
using Newtonsoft.Json;
using ParimatchDayOddsParser.Parimatch;
using System;
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
            var hm = JsonConvert.DeserializeObject<CyberFootballHistoricalMatchResult[]>(File.ReadAllText($@"C:\BetEventScanner\cyberFootball\results\index.json"));
            //var c = hm.Count(x => x.Date == null);

            var hist = new HistoricalResultsProcessor();
            var cyberStarFootball = new PmCyberStarFootballProcessor(new BetEventScanner.Providers.FifaonlinecupOrg.Service(), new CyberFootballBetsProcessor());
            var eSportBattleFootball = new PmEsportBattleFootballProcessor(new CyberFootballBetsProcessor());
            var pmLive = new ParimatchLiveBetProcessor();

            var cyberLive = false;
            var esportBattler = true;
            var generalLive = false;

            while (true)
            {
                try
                {
                    Console.Clear();

                    hist.Process();
                    return;

                    if (cyberLive)
                        cyberStarFootball.Process();

                    if (esportBattler)
                        eSportBattleFootball.Process();

                    if (generalLive)
                        pmLive.Process();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                await Task.Delay(TimeSpan.FromSeconds(61));
            }
        }
    }
}
