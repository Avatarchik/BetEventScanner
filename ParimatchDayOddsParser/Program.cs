using BetEventScanner.Providers.Parimatch;
using BetEventScanner.Providers.Parimatch.Model;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParimatchDayOddsParser
{
    public class IncomingBetEventsProcessor
    {
        private ChromeDriver driver;

        public void ProcessGettingIncomingBetEvents()
        {
            var url = "https://www.parimatch.com/en/bet.html?filter=today";

            string sourceHtml = null;

            using (driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(url);
                sourceHtml = driver.PageSource;
            }

            if (sourceHtml == null) throw new Exception("Parimatch source html not loaded");

            var incomingBetEvents = new ParimatchProvider(new ParimatchSettings()).GetFutureOddsBetEvents(sourceHtml);

            var casted = incomingBetEvents.OfType<ParimatchTennisBetEvent>().ToList();

            new FutureOddsBetEventsStorage().Store(casted);
        }
    }

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    new IncomingBetEventsProcessor().ProcessGettingIncomingBetEvents();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                Task.Delay(TimeSpan.FromHours(1)).GetAwaiter().GetResult();
            }
        }
    }
}
