using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using BetEventScanner.DataAccess.Providers;
using BetEventScanner.DataModel.Model;
using BetEventScanner.Providers.Contracts;
using BetEventScanner.Providers.SoccerStandCom.Model;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace BetEventScanner.Providers.SoccerStandCom
{
    public enum ConverterParth
    {
        MatchSummary,
        Statistics,
        Odds
    }

    public interface IConverter<out T>
    {
        ConverterParth Prth { get; }

        T Convert(string html);
    }

    public class MatchSummaryConverter : IConverter<MatchSummary>
    {
        public ConverterParth Prth { get; } = ConverterParth.MatchSummary;

        public MatchSummary Convert(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var matchSummary = new MatchSummary();
            matchSummary.FinalScore = htmlDocument.GetElementbyId("event_detail_current_result").InnerText.Replace("-", ":");

            var root = htmlDocument.GetElementbyId("summary-content");
            var partsTableElements = root.ChildNodes[0].ChildNodes.Nodes().ToList();
            
            for (var i = 0; i < partsTableElements.Count; i++)
            {
                var attrs = partsTableElements[i].Attributes.ToList();
                if (attrs.Any(x=>x.Name == "class" && x.Value == "stage-header stage-12"))
                {
                    matchSummary.FirstHalfScore = partsTableElements[i + 1].ChildNodes[1].InnerText.Replace("-", ":");
                    continue;
                }

                if (!attrs.Any(x => x.Name == "class" && x.Value == "stage-header stage-13")) continue;

                matchSummary.SecondHalfScore = partsTableElements[i + 1].ChildNodes[1].InnerText.Replace("-", ":");
                break;
            }

            return matchSummary;
        }
    }

    public class StatisticsConverter : IConverter<MatchStatistics>
    {
        public ConverterParth Prth { get; } = ConverterParth.Statistics;

        public MatchStatistics Convert(string html)
        {
            return new MatchStatistics();
        }
    }

    public class MatchStatistics
    {
        public IDictionary<FootballMatchStage, MatchStageStatistics> StageStatistics { get; set; }
    }

    public class MatchStageStatistics
    {
        public string BallPossession { get; set; }

        public string GoalAttempts { get; set; }

        public string ShotsOnGoal { get; set; }

        public string ShotsOffGoal { get; set; }

        public string BlockedShots { get; set; }

        public string FreeKicks { get; set; }

        public string CornerKicks { get; set; }

        public string Offsides { get; set; }

        public string GoalkeeperSaves { get; set; }

        public string Fouls { get; set; }

        public string YellowCards { get; set; }

        public string TotalPasses { get; set; }

        public string Tackles { get; set; }
    }

    public class MatchSummary
    {
        public string FinalScore { get; set; }

        public string FirstHalfScore { get; set; }

        public string SecondHalfScore { get; set; }

        public string Referee { get; set; }

        public string Venue { get; set; }

        public string Attendance { get; set; }
    }

    public class SoccerStandParser
    {
        private readonly IParserStorage<SoccerstandData> _storage;
        private readonly ParseSettings _settings;

        public SoccerStandParser(IParserStorage<SoccerstandData> storaSorage, ParseSettings settings)
        {
            _storage = storaSorage;
            _settings = settings;
        }

        public ICollection<string> ParseCountryUrls()
        {
            var leftMenuSelector = ".menu.menu-left > li > ul > li >  a";
            var url = "https://www.soccerstand.com/";

            var driver = new ChromeDriver();

            driver.Navigate().GoToUrl(url);

            var menuElements = driver.FindElements(By.CssSelector(leftMenuSelector)).ToList();

            var res = new List<string>();

            foreach (var menuElement in menuElements)
            {
                var href = menuElement.GetAttribute("href");
                res.Add(href);
            }

            return res;
        }

        public void ParseCurrentSeasons()
        {
            var urls = ParseCountryUrls();

            foreach (var url in urls)
            {
                try
                {
                    if (url.ToUpper().Contains("CUP"))
                    {
                        continue;
                    }

                    var data = ParseCurrentSeason(url);
                    data.SetupAdditionalInfo();

                    var storage = new SoccerstandMongoStorage();
                    storage.StoreDataToCollection(data.Country, new List<SoccerstandData> { data });
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }
            }
        }

        public SoccerstandData ParseCurrentSeason(string baseUrl)
        {
            var driver = new ChromeDriver();

            var data = new SoccerstandData
            {
                Url = baseUrl
            };

            data.Matches.AddRange(ParseResults(driver, baseUrl + "results/"));
            data.Matches.AddRange(ParseFixtures(driver, baseUrl + "fixtures/"));

            driver.Close();

            return data;
        }

        public ICollection<SoccerstandMatch> ParseFixtures(ChromeDriver driver, string url)
        {
            var stageSelector = ".tournament_part";
            var fixtureTableSelector = "#fs-fixtures > table > tbody > tr";

            driver.Navigate().GoToUrl(url);
            ExpandAllMatches(driver);

            var stage = driver.FindElement(By.CssSelector(stageSelector)).Text;
            var fixtureTable = driver.FindElements(By.CssSelector(fixtureTableSelector)).ToList();

            var roundOrigin = string.Empty;
            var round = 0;

            var result = new List<SoccerstandMatch>();

            foreach (var fixture in fixtureTable)
            {
                var matchClass = fixture.GetAttribute("class");
                if (matchClass == "event_round")
                {
                    roundOrigin = fixture.Text;
                    round = int.Parse(roundOrigin.Split(' ')[1]);
                    continue;
                }

                var id = fixture.GetAttribute("id");
                var matchId = id.Replace("g_1_", "");

                var fixtureData = fixture.Text.Split(new[] { "\r\n" }, StringSplitOptions.None);

                var dateTemp = fixtureData[0].Split(' ').ToList();
                var date = DateTime.Parse(dateTemp[0] + "18 " + dateTemp[1]);
                var homeTeam = fixtureData[1];
                var awayTeam = fixtureData[2];

                var match = new SoccerstandMatch
                {
                    DateTime = date,
                    OriginId = matchId,
                    Stage = stage,
                    RoundOrigin = roundOrigin,
                    Round = round,
                    HomeTeam = homeTeam,
                    AwayTeam = awayTeam,
                    Type = SoccerstandMatchType.Fixture,
                    SourceProvider = SourceProvider.Soccerstand
                };

                result.Add(match);
            }

            return result;
        }

        public ICollection<SoccerstandMatch> ParseResults(ChromeDriver driver, string url)
        {
            var seeMore = "#tournament-page-results-more > tbody > tr > td > a";
            var leagueTables = "#fs-results > table > tbody > tr";
            var stageSelector = ".tournament_part";
            var yearsSelector = "h2.navigation";

            driver.Navigate().GoToUrl(url);
            ExpandAllMatches(driver);

            var stage = driver.FindElement(By.CssSelector(stageSelector)).Text;
            var matchResults = driver.FindElements(By.CssSelector(leagueTables)).ToList();

            var navigationSegments = driver.FindElement(By.CssSelector(yearsSelector)).Text;
            var years = navigationSegments.Split('»').Last().Split('/').Select(int.Parse).OrderBy(x => x).ToList();

            var roundOrigin = string.Empty;
            var round = 0;

            var result = new List<SoccerstandMatch>();

            foreach (var matchResult in matchResults)
            {
                var matchClass = matchResult.GetAttribute("class");
                if (matchClass == "event_round")
                {
                    roundOrigin = matchResult.Text;
                    round = int.Parse(roundOrigin.Split(' ')[1]);
                    continue;
                }

                var id = matchResult.GetAttribute("id");
                var matchId = id.Replace("g_1_", "");

                var resultData = matchResult.Text.Split(new[] { "\r\n" }, StringSplitOptions.None);

                var date = resultData[0];
                var homeTeam = resultData[1];
                var awayTeam = resultData[2];
                var resultOrigin = resultData[3];
                var resultOriginSplit = resultOrigin.Split(':');
                var homeScored = int.Parse(resultOriginSplit[0]);
                var awayScored = int.Parse(resultOriginSplit[1]);

                var match = new SoccerstandMatch
                {
                    DateTimeOrigin = date,
                    OriginId = matchId,
                    Stage = stage,
                    RoundOrigin = roundOrigin,
                    Round = round,
                    HomeTeam = homeTeam,
                    AwayTeam = awayTeam,
                    ResultOrigin = resultOrigin,
                    HomeScored = homeScored,
                    AwayScored = awayScored,
                    Type = SoccerstandMatchType.Result,
                    SourceProvider = SourceProvider.Soccerstand
                };

                result.Add(match);
            }

            var maxRound = result.Max(x => x.Round);
            var source = result.OrderBy(x => x.Round).ToList();
            var startMonth = 0;
            for (var i = 0; i < source.Count; i++)
            {
                var match = source[i];
                var dateTemp = match.DateTimeOrigin.Split(' ').ToList();
                if (match.Round == 1)
                {
                    match.DateTime = DateTime.Parse($"{dateTemp[0]}{years.Min()} {dateTemp[1]}");
                    startMonth = match.DateTime.Month;
                    continue;
                }
                if (match.Round == maxRound)
                {
                    match.DateTime = DateTime.Parse($"{dateTemp[0]}{years.Max()} {dateTemp[1]}");
                    continue;
                }

                var month = int.Parse(match.DateTimeOrigin.Split('.')[1]);
                if (month >= startMonth && month <= 12)
                {
                    match.DateTime = DateTime.Parse($"{dateTemp[0]}{years.Min()} {dateTemp[1]}");
                }
                else
                {
                    match.DateTime = DateTime.Parse($"{dateTemp[0]}{years.Max()} {dateTemp[1]}");
                }
            }

            return result;
        }

        private static void ExpandAllMatches(ChromeDriver driver)
        {
            var showMoreSelectrors = new List<string>
            {
                "#tournament-page-summary-results-more  > tbody > tr > td > a",
                "#tournament-page-results-more  > tbody > tr > td > a",
                ".link-more-games > tbody > tr > td > a",
            };

            while (true)
            {
                IWebElement clickElement;

                var elements = driver.FindElements(By.CssSelector(showMoreSelectrors[2]));
                Thread.Sleep(200);
                if (elements == null || elements.Count == 0)
                {
                    elements = driver.FindElements(By.CssSelector(showMoreSelectrors[1]));
                    Thread.Sleep(200);
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
                Thread.Sleep(5000);

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

        public void UpdateMatchDetails()
        {
            var colName = "england";
            var provider = new MongoDbProvider("footballdb");
            var collections = provider.GetEntities<SoccerstandData>(colName);

            var mapping = new Dictionary<string, ConverterParth>
            {
                { "#match-summary", ConverterParth.MatchSummary },
                { "#match-statistics;0", ConverterParth.Statistics },
                { "#odds-comparison", ConverterParth.Odds }
            };

            foreach (var collection in collections)
            {
                foreach (var soccerstandMatch in collection.Matches)
                {
                    if (soccerstandMatch.Type == SoccerstandMatchType.Fixture)
                    {
                        continue;
                    }

                    var driver = new ChromeDriver();

                    foreach (var mapItem in mapping)
                    {
                        var url = $"https://www.soccerstand.com/match/{soccerstandMatch.OriginId}/{mapItem.Key}";
                        driver.Navigate().GoToUrl(url);
                        var source = driver.PageSource;

                        switch (mapItem.Value)
                        {
                            case ConverterParth.MatchSummary:
                                var matchSummary = new MatchSummaryConverter().Convert(source);
                                soccerstandMatch.Referee = matchSummary.Referee;
                                soccerstandMatch.Venue = matchSummary.Venue;
                                soccerstandMatch.Attendance = matchSummary.Attendance;
                                break;

                            case ConverterParth.Statistics:
                                break;
                            case ConverterParth.Odds:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }
        }
    }
}