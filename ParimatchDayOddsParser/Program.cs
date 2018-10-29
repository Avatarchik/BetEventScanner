using BetEventScanner.Providers.Parimatch;
using BetEventScanner.Providers.Vprognoze;
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
            while (true)
            {
                try
                {
                    //ParseParimatch();
                    ParseVprognoze();
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
            var html = HtmlParser.ParseWebDriverFromAttribute("https://air.parimatch.com/en/prematch/0-24/");
            var parimatch = new AirParimatchProvider();

            var prematchEvents = parimatch.ParsePreMatchOdds(html);
        }

        private static void ParseVprognoze()
        {
            var html = HtmlParser.ParseWebDriver("http://vprognoze.ru/statalluser/");
            var users = new VprProvider().GetCurrentTopUsers(html);
        }
    }
}
