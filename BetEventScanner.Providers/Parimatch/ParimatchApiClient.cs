using System;
using System.IO;
using System.Net;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BetEventScanner.Providers.Parimatch
{
    public class ParimatchApiClient
    {
        //public ICollection<ParimatchFootballBetEvent> GetArchiveBetEvents(DateTime date)
        //{
        //    return null;
        //}

        //public ICollection<ParimatchFootballBetEvent> GetArchiveBetEvents(ICollection<DateTime> dates)
        //{
        //    return null;
        //}

        public string DownloadHtml(string url)
        {
            var web = new HtmlWeb
            {
                BrowserTimeout = TimeSpan.FromMinutes(30)
            };
            var html = web.LoadFromBrowser(url);
            return html.ParsedText;
        }

        public string DownloadHtmlWC(string url)
        {
            using (var wc = new WebClient())
            {
                return wc.DownloadString(url);
            }
        }

        public string DownloadHtmlSelenium(string url)
        {
            string sourceHtml = null;

            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(url);
                sourceHtml = driver.PageSource;
            }
            
            return sourceHtml;
        }

        public string DownloadHtmlHWR(string url)
        {
            var response = "";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            using (Stream stream = request.GetResponse().GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    response = reader.ReadToEnd();
                }
            }

            return response;
        }
    }
}
