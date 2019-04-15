using BetEventScanner.Providers.FifaonlinecupOrg;
using System;
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
            var cyberStarFootball = new PmCyberStarFootballProcessor(new Service(), new CyberFootballBetsProcessor());
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
