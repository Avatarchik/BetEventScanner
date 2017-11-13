using System;
using System.Collections.Generic;
using BetEventScanner.SoccerstandScaner.Contracts;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SoccerStandParser
{
    public class SoccerStandMatchConverter
    {
        public void Convert(IDataSource dataSource)
        {
            var source = dataSource.GetSourceData();

            var driver = new ChromeDriver();
            foreach (var soccerStandMatch in source.SoccerStandMatches)
            {
                driver.Navigate().GoToUrl($"https://www.soccerstand.com/match/{soccerStandMatch.Id}/#match-summary");
                var actions21 = new List<string>
                    {
                        "content-all",
                        "a-match-odds-comparison",
                        "bookmark-under-over",
                        "bookmark-asian-handicap",
                        "bookmark-correct-score",
                        "bookmark-both-teams-to-score"
                    };

                foreach (var action in actions21)
                {
                    var actionElement = TryGetWebElement(driver, action);
                    var actionElementHtml = actionElement.GetAttribute("innerHTML");
                }

                driver.Close();
            }
        }

        private static IWebElement TryGetWebElement(ChromeDriver driver, string id)
        {
            var retryCount = 0;

            while (true)
            {
                try
                {
                    var element = driver.FindElement(By.Id(id));
                    if (element.Displayed)
                    {
                        return element;
                    }

                    if (++retryCount == 3)
                    {
                        throw new InvalidOperationException($"Can't get element {id}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}