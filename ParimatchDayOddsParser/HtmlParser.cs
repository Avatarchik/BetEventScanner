using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
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

        public static string GetElementById(string url, string id)
        {
            Connect(url);

            var e = driver?.FindElementById(id);

            return e.GetAttribute("innerHTML");
        }

        public static string[] GetElementsByIds(string url, string[] ids)
        {
            Connect(url);

            var r = new List<string>();

            foreach (var id in ids)
            {
                var e = driver?.FindElementsById(id)?.FirstOrDefault();
                if (e == null) continue;
                r.Add(e.GetAttribute("innerHTML"));
            }

            return r.ToArray();
        }

        public static string GetElementByTagName(string url, string tag)
        {
            Connect(url);
            var els = driver.FindElementsByTagName(tag);

            var sourceHtml = els.First().GetAttribute("innerHTML");

            return sourceHtml;
        }

        private static void Connect(string url)
        {
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
        }

        private static string ParseWebDriverFromAttribute(string url, string anchor)
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

            sourceHtml = els.First().GetAttribute("innerHTML");

            return sourceHtml;
        }



    }
}
