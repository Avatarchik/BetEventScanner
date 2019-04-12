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
            var parimatchCyberFootball = new ParimatchCyberFootballProcessor(new Service());
            var pmLive = new ParimatchLiveBetProcessor();

            var cyberLive = true;
            var generalLive = false;

            while (true)
            {
                try
                {
                    Console.Clear();

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
    }
}
