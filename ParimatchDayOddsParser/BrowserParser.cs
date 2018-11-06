using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

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
}
