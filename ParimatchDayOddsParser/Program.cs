using BetEventScanner.Providers.Parimatch;
using BetEventScanner.Providers.Parimatch.Model;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParimatchDayOddsParser
{
    public class HtmlParser
    {
        private static ChromeDriver driver = null;

        public static string ParseWebDriver(string url)
        {
            string sourceHtml = null;

            if (driver == null)
            {
                driver = new ChromeDriver();
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
                driver.Navigate().GoToUrl(url);

                var els = driver.FindElementsByTagName("prematch-country");
                Console.WriteLine(els.First().TagName);
                Console.WriteLine(els.First().Text);
            }
            else
            {
                driver.Navigate().Refresh();
            }

            sourceHtml = driver.PageSource;

            return sourceHtml;
        }

        public static string ParseWebDriverWait(string url)
        {
            string sourceHtml = null;

            if (driver == null)
            {
                driver = new ChromeDriver();
                driver.Navigate().GoToUrl(url);
            }

            driver.Navigate().Refresh();
            sourceHtml = driver.PageSource;

            if (sourceHtml == null) throw new Exception("Parimatch source html not loaded");

            return sourceHtml;
        }

    }

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            //var parimatch = new OldParimatchProvider(new ParimatchSettings());
            var parimatch = new AirParimatchProvider();

            while (true)
            {
                try
                {
                    //var html = HtmlParser.ParseWebDriver("https://www.parimatch.com/en/bet.html?filter=today");
                    var html = HtmlParser.ParseWebDriver("https://air.parimatch.com/prematch/0-24/");

                    var incomingBetEvents = parimatch.ParsePreMatchOdds(html);

                    //var casted = incomingBetEvents.OfType<ParimatchTennisBetEvent>().ToList();

                    //new FutureOddsBetEventsStorage().Store(casted);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }
    }
}
