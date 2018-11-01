using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

namespace ParimatchDayOddsParser
{
    public class HtmlParser
    {
        private static ChromeDriver driver = null;

        public static string ParseWebDriver(string url)
        {
            if (driver == null)
            {
                driver = new ChromeDriver();
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
            }

            driver.Navigate().GoToUrl(url);

            return driver.PageSource;
        }

        public static string ParseWebDriverFromAttribute(string url, string anchor)
        {
            string sourceHtml = null;

            if (driver == null)
            {
                driver = new ChromeDriver();
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
                driver.Navigate().GoToUrl(url);

            }
            else
            {
                driver.Navigate().Refresh();
            }

            var els = driver.FindElementsByTagName(anchor);

            //var el1 = els.First();
            sourceHtml = els.First().GetAttribute("innerHTML");

            return sourceHtml;
        }



    }
}
