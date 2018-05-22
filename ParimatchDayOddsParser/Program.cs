using BetEventScanner.Providers.Parimatch;
using System;
using System.Threading.Tasks;

namespace ParimatchDayOddsParser
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            while (true)
            {
                var parimatch = new ParimatchProvider(new ParimatchSettings());
                parimatch.DownloadTodayOdds();

                Task.Delay(TimeSpan.FromHours(6)).GetAwaiter().GetResult();
            }
        }
    }
}
