using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BetEventScanner.Common.Contracts.Services;
using BetEventScanner.Common.DataModel;
using BetEventScanner.Common.Services.Common;
using BetEventScanner.Common.Services.FootbalDataCoUk.Model;
using BetEventScanner.DataAccess.DataModel;
using BetEventScanner.DataModel;
using FootballMatchResult = BetEventScanner.DataModel.FootballMatchResult;
using Status = BetEventScanner.Common.Services.FootbalDataCoUk.Model.Status;

namespace BetEventScanner.Common.Services.FootbalDataCoUk
{
    public class FootballDataCoUkService : IFootballService, IHistoricalResultsDataSource
    {
        public readonly string Url = "http://www.football-data.co.uk/";
        private DataSourceFootballDataCoUk _dataSource = new DataSourceFootballDataCoUk();
        private readonly IFileService _fileService = new FileService();
        private IEnumerable<string> _supportedLeagues = new List<string> {"E0"/*, "E1", "E2", "E3", "E1", "EC"*/ };
        private IResultsService _resultsService;
        private string directory = "c:\\BetEventScanner\\Services\\FootballDataCoUk";
        private string statusFile = "status.xml";

        public string Name { get; } = "FootballDataCoUk";

        public FootballDataCoUkService()
        {
            Init();
        }

        private void Init()
        {
            var status = _fileService.ReadFile<Status>(Path.Combine(directory, statusFile));

            if (status != null && status.Initialized) return;

            CheckStatusFile();
            DownloadFiles();
        }

        public IEnumerable<DataAccess.DataModel.FootballMatchResult> GetAllResults()
        {
           return new List<DataAccess.DataModel.FootballMatchResult>();
        }

        public IEnumerable<DataAccess.DataModel.FootballMatchResult> GetDivisionResults(CountryDivision countryDivision, DateTime fromDate)
        {
            return new List<DataAccess.DataModel.FootballMatchResult>();
        }

        private void DownloadFiles()
        {
            var startYear = 93;
            var endYear = 94;
            var startYearStr = "93";
            var endYearStr = "94";

            var curYear = DateTime.Now.Year.ToString().Remove(0,2);

            var dataDirectory = directory + "\\Data";
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            while (true)
            {
                foreach (var supportedLeague in _supportedLeagues)
                {
                    var fileName = GetFileName(supportedLeague, startYearStr, endYearStr);
                    var fileNameToStore = $"{supportedLeague}_{startYearStr}{endYearStr}.csv";
                    _dataSource.DownloadFile(Url + fileName, fileNameToStore);
                    if (File.Exists(fileNameToStore))
                    {
                        var destFilePath = Path.Combine(dataDirectory, fileNameToStore);
                        if (!File.Exists(destFilePath))
                        {
                            _fileService.MoveFile(fileNameToStore, destFilePath);
                        }
                    }
                }

                startYearStr = ++startYear >= 100 ? startYear.ToString().Remove(0, 1) : startYear.ToString();

                endYearStr = ++endYear >= 100 ? endYear.ToString().Remove(0, 1) : endYear.ToString();
                
                if (startYearStr == curYear )
                {
                    break;
                }
            }

            //var downloadedFileName = $"mmz4281/9293/EC.csv";
            //var targetUrl = $"{Url}{downloadedFileName}";
            //_dataSource.DownloadFile(targetUrl, "temp123.csv");
        }

        private string GetFileName(string  league, string startYear, string endYear)
        {
            return $"mmz4281/{startYear}{endYear}/{league}.csv";
        }

        private void CheckStatusFile()
        {
            var statusFilePath = Path.Combine(directory, statusFile);

            if (File.Exists(statusFilePath)) return;

            if (!Directory.Exists(directory))
            {
                if (directory != null) Directory.CreateDirectory(directory);
            }
                
            _fileService.SaveFile(statusFilePath, new Status
            {
                IsUpdated = false,
                LastCheck = DateTime.Now,
                CreationDateTime = DateTime.Now
            });
        }

        public ICollection<FootballMatchResult> GetHistoricalMatches(string filePath)
        {
            var results = FootballDataCoUkParser.GetHistoricalResults(filePath);
            var parsingErrors = false;
            return results.Select(x =>
            {
                int homeScored;
                if (!int.TryParse(x.FTHG, out homeScored))
                {
                    homeScored = 0;
                    parsingErrors = true;
                }

                int awayScored;
                if (!int.TryParse(x.FTAG, out awayScored))
                {
                    awayScored = 0;
                    parsingErrors = true;
                }

                double homeOdds;
                if (!double.TryParse(x.B365H, out homeOdds))
                {
                    homeOdds = 0.0;
                    parsingErrors = true;
                }

                double drawOdds;
                if (!double.TryParse(x.B365D, out drawOdds))
                {
                    drawOdds = 0.0;
                    parsingErrors = true;
                }

                double awayOdds;
                if (!double.TryParse(x.B365A, out awayOdds))
                {
                    awayOdds = 0.0;
                    parsingErrors= true;
                }

                double over25Odds;
                if (!double.TryParse(x.BbAvOver25, out over25Odds))
                {
                    over25Odds = 0.0;
                    parsingErrors= true;
                }

                double under25Odds;
                if (!double.TryParse(x.BbAvUnder25, out under25Odds))
                {
                    under25Odds = 0.0;
                    parsingErrors= true;
                }

                return new FootballMatchResult
                {
                    Div = x.Div,
                    DateTime = DateTime.Parse(x.Date),
                    HomeTeam = x.HomeTeam,
                    AwayTeam = x.AwayTeam,
                    HomeScored = homeScored,
                    AwayScored = awayScored,
                    HomeOdds = homeOdds,
                    DrawOdds = drawOdds,
                    AwayOdds = awayOdds,
                    Over25Odds = over25Odds,
                    Under25Odds = under25Odds,
                    ParsingErrors = parsingErrors
                };

            }).ToList();
        }

        public ICollection<FootballMatchResult> GetFixtures(string filePath)
        {
            var fixtures = FootballDataCoUkParser.GetFixture(filePath);
            return fixtures.Select(ConvertToMatchResult).ToList();
        }

        private static FootballMatchResult ConvertToMatchResult(FixtureMatch fixtureMatch)
        {
            var parsingErrors = false;

            int homeScored;
            if (!int.TryParse(fixtureMatch.FTHG, out homeScored))
            {
                homeScored = 0;
                parsingErrors = true;
            }

            int awayScored;
            if (!int.TryParse(fixtureMatch.FTAG, out awayScored))
            {
                awayScored = 0;
                parsingErrors = true;
            }

            double homeOdds;
            if (!double.TryParse(fixtureMatch.B365H, out homeOdds))
            {
                homeOdds = 0.0;
                parsingErrors = true;
            }

            double drawOdds;
            if (!double.TryParse(fixtureMatch.B365D, out drawOdds))
            {
                drawOdds = 0.0;
                parsingErrors = true;
            }

            double awayOdds;
            if (!double.TryParse(fixtureMatch.B365A, out awayOdds))
            {
                awayOdds = 0.0;
                parsingErrors = true;
            }

            double over25Odds;
            if (!double.TryParse(fixtureMatch.BbAvOver25, out over25Odds))
            {
                over25Odds = 0.0;
                parsingErrors = true;
            }

            double under25Odds;
            if (!double.TryParse(fixtureMatch.BbAvUnder25, out under25Odds))
            {
                under25Odds = 0.0;
                parsingErrors = true;
            }

            return new FootballMatchResult
            {
                Div = fixtureMatch.Div,
                DateTime = DateTime.Parse(fixtureMatch.Date),
                HomeTeam = fixtureMatch.HomeTeam,
                AwayTeam = fixtureMatch.AwayTeam,
                HomeScored = homeScored,
                AwayScored = awayScored,
                HomeOdds = homeOdds,
                DrawOdds = drawOdds,
                AwayOdds = awayOdds,
                Over25Odds = over25Odds,
                Under25Odds = under25Odds,
                OverallTotal = homeScored + awayScored,
                ParsingErrors = parsingErrors
            };
        }
    }
}