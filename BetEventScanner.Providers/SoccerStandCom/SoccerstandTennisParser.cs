using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace BetEventScanner.Providers.SoccerStandCom
{
    public class SoccerstandTennisParser
    {
        public void Parse()
        {
            var url = "https://www.soccerstand.com/tennis";
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            var html = driver.PageSource;

            var nds = driver.FindElementsByCssSelector("table[class=tennis]").Cast<RemoteWebElement>().ToList();

            var res = new List<SoccerstandTennisMatch>();

            foreach (var item in nds)
            {
                var innerHtml = item.GetAttribute("innerHTML");
                var matches = Convert(innerHtml);

                res.AddRange(matches);
            }

        }

        private List<SoccerstandTennisMatch> Convert(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nameContainer = doc.QuerySelector("span[class=name]");
            var countryPart = nameContainer.QuerySelector("span[class=country_part]").InnerText;
            var tournamentPart = nameContainer.QuerySelector("span[class=tournament_part]").InnerText;

            var matches = doc.QuerySelectorAll("tbody > tr").Where(x => !string.IsNullOrEmpty(x.Id)).ToList();
            
            return new List<SoccerstandTennisMatch>();
        }


    }
}