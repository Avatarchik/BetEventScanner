using BetEventScanner.Providers.Parimatch;
using BetEventScanner.Providers.Vprognoze;
using BetEventScanner.Providers.Vprognoze.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParimatchDayOddsParser
{
    public class BrowserParser
    {
        private BlockingCollection<string> _parsingQueue = new BlockingCollection<string>();
        private Queue<string> _processed = new Queue<string>();

        private ChromeDriver _driver;

        private IDictionary<string, int> _tabs = new Dictionary<string, int>();

        public string GetHtml(string url)
        {
            CreateDriver();
            Navigate(url);

            return _driver.PageSource;
        }

        private void CreateDriver()
        {
            if (_driver == null)
            {
                _driver = new ChromeDriver();
                _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
            }
        }

        private void Navigate(string url)
        {
            SelectTab(url);
            _driver.Navigate().GoToUrl(url);
        }

        private void SelectTab(string url)
        {
            var uri = new Uri(url);
            string t = uri.GetLeftPart(UriPartial.Authority);
            if (!_tabs.ContainsKey(t))
            {
                var num = _tabs.Count;
                _tabs.Add(t, ++num);
            }

            _driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + "t");
            _driver.SwitchTo().Window(_driver.WindowHandles[_tabs[t]]);
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
            var vpr = new VprProvider();
            var bettors = vpr.GetCurrentTopUsers(html);

            var d = new Dictionary<Bettor, Bet[]>();

            foreach (var bettor in bettors)
            {
                var link = vpr.GetBettorBetLink(bettor);
                var betsHtml = HtmlParser.ParseWebDriver(link);
                var bettotBets = vpr.ParseBettorBets(bettor, betsHtml);

                d.Add(bettor, bettotBets);

            }
        }
    }
}
