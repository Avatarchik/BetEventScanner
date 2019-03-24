using BetEventScanner.Providers;
using BetEventScanner.Providers.Parimatch;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParimatchDayOddsParser
{
    public class ParimatchWebBrowser
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

        internal static string GetPageHtml(string url)
        {
            if (driver == null)
            {
                OpenRefresh(url);
            }

            return driver.PageSource;
        }

        public static HtmlNode GetElementById(string url, string id, bool inTab = false)
        {
            OpenRefresh(url);

            var e = driver?.FindElementById(id);

            return new HtmlNode
            {
                Key = id,
                Html = e.GetAttribute("innerHTML")
            };
        }

        public static HtmlNode[] GetElementsByIds(string url, string[] ids, bool inTab = false)
        {
            if (driver == null)
            {
                OpenRefresh(url);
            }

            var r = new List<HtmlNode>();

            foreach (var id in ids)
            {
                var e = driver?.FindElementsById(id)?.FirstOrDefault();
                if (e == null) continue;
                r.Add(new HtmlNode
                {
                    Key = id,
                    Html = e.GetAttribute("innerHTML")
                });
            }

            return r.ToArray();
        }

        public static void Login(ParimatchCredentials creds)
        {
            OpenRefresh("https://www.parimatch.com/?login=1");
            var n = driver.FindElementByName("username");
            n.SendKeys(creds.User);
            var p = driver.FindElementByName("passwd");
            p.SendKeys(creds.Pasw);
            driver.FindElementByCssSelector("button.ok").Click();
        }

        public static void PlaceBet(string envo)
        {
            OpenRefresh("https://www.parimatch.com");
            OpenTab($"https://www.parimatch.com/bet.html?ARDisabled=on&hl={envo}");
            var f1 = driver?.FindElementById("f1").GetAttribute("innerHTML");
            var internalId = f1.GetCssNode("div.container > a").Attributes["name"].Value;
            var headers = f1.GetCssNode($"table#g{internalId} > tbody");
            var main = f1.GetCssNode($"table#g{internalId} > tbody.row1");
            var props = f1.GetCssNode($"table#g{internalId} > tbody.row1.props");
            var originBetId = main.InnerHtml.GetCssNode("a").Attributes["id"].Value;
            var betId = originBetId.Split('_').Last();
            OpenTab($"https://www.parimatch.com/stake.html?ev={betId}&bm=2");
            driver.FindElementByName("sums").SendKeys("3");
            driver.FindElementById("do_stake").Click();
        }

        public static string GetElementByTagName(string url, string tag, bool inTab = false)
        {
            OpenRefresh(url);
            var els = driver.FindElementsByTagName(tag);

            var sourceHtml = els.First().GetAttribute("innerHTML");

            return sourceHtml;
        }

        private static void OpenRefresh(string url)
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

        private static void OpenTab(string url)
        {
            driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + "t");
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Navigate().GoToUrl(url);
        }

        private static void SwitchBack()
        {
            driver.SwitchTo().Window(driver.WindowHandles.First());
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

        public static string GetElementByCssSelector(string url, string selector)
        {
            if (driver == null)
            {
                OpenRefresh(url);
            }
            
            var e = driver.FindElementByCssSelector(selector);

            var sourceHtml = e.GetAttribute("innerHTML");

            return sourceHtml;
        }
    }
}
