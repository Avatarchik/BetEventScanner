using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using SoccerStandParser;

namespace BetEventScanner.SoccerstandScaner
{
    public class SoccerStandParser
    {
        private readonly IParserStorage<SoccerstandData> _storaSorage;
        private readonly ParseSettings _settings;

        public SoccerStandParser(IParserStorage<SoccerstandData> storaSorage, ParseSettings settings)
        {
            _storaSorage = storaSorage;
            _settings = settings;
        }

        public void Parse()
        {
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(_settings.Url);

            var seeMore = "#tournament-page-results-more > tbody > tr > td > a";
            var leagueTables = "#fs-results > table";

            var data = new SoccerstandData
            {
                Url = _settings.Url
            };

            ExpandAllMatches(driver);

            var selectedRows = driver.FindElements(By.CssSelector(leagueTables)).ToList();
            foreach (var selectedRow in selectedRows)
            {
                var stage = selectedRow.FindElement(By.ClassName("tournament_part")).Text;
                var roundMatches = selectedRow.FindElements(By.CssSelector("tBody > tr")).ToList();
                var round = "";
                foreach (var matchRow in roundMatches)
                {
                    var match = new SoccerStandMatch
                    {
                        Stage = stage
                    };

                    var matchClass = matchRow.GetAttribute("class");
                    if (matchClass == "event_round")
                    {
                        round = matchRow.Text;
                        continue;
                    }

                    var id = matchRow.GetAttribute("id");
                    var matchId = id.Replace("g_1_", "");
                    var element = driver.FindElementById(id);

                    var matchHtml = element.GetAttribute("innerHTML");

                    match.Id = matchId;
                    match.Round = round;
                    match.MatchHtml = matchHtml;

                    data.SoccerStandMatches.Add(match);
                }
            }

            _storaSorage.Store(data);

            driver.Close();
        }

        private static void ExpandAllMatches(ChromeDriver driver)
        {
            while (true)
            {
                IWebElement clickElement;


                var elements = driver.FindElements(By.CssSelector("#tournament-page-summary-results-more  > tbody > tr > td > a"));
                if (elements == null || elements.Count == 0)
                {
                    elements = driver.FindElements(By.CssSelector("#tournament-page-results-more  > tbody > tr > td > a"));
                    if (elements == null || elements.Count == 0)
                    {
                        break;
                    }

                    clickElement = elements.First();
                }
                else
                {
                    clickElement = elements.First();
                }

                if (clickElement == null)
                {
                    break;
                }

                var actions = new Actions(driver);
                actions.MoveToElement(clickElement);
                actions.Perform();

                if (clickElement.Displayed)
                {
                    clickElement.Click();
                }
                else
                {
                    break;
                }
            }
        }
    }
}