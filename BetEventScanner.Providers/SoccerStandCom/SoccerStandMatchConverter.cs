using System;
using System.Collections.Generic;
using BetEventScanner.Providers.Contracts;
using BetEventScanner.Providers.SoccerStandCom.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BetEventScanner.Providers.SoccerStandCom
{
    public class SoccerStandMatchConverter
    {
        public void Convert(IDataSource<SoccerstandData> dataSource)
        {
            var source = dataSource.GetSourceData();

            var driver = new ChromeDriver();

            foreach (var item in source)
            {
                foreach (var soccerStandMatch in item.Matches)
                {
                    driver.Navigate().GoToUrl($"https://www.soccerstand.com/match/{soccerStandMatch.OriginId}/#match-summary");
                    
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