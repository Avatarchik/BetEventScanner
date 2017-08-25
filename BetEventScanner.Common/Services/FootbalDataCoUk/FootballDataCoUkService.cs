using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.Contracts.Services;
using BetEventScanner.Common.DataModel;
using BetEventScanner.Common.ResultsService;
using BetEventScanner.Common.Services.FootbalDataCoUk.Dto;
using BetEventScanner.DataModel;

namespace BetEventScanner.Common.Services.FootbalDataCoUk
{
    public class FootballDataCoUkService : IFootballService, IHistoricalResultsDataSource
    {
        public readonly string Url = "http://www.football-data.co.uk/";
        private DataSourceFootballData _dataSource = new DataSourceFootballData();
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
            var status = _fileService.ReadFile<StatusDto>(Path.Combine(directory, statusFile));

            if (status != null && status.Initialized) return;

            CheckStatusFile();
            DownloadFiles();
        }

        public IEnumerable<IMatchResult> GetAllResults()
        {
           return new List<IMatchResult>();
        }

        public IEnumerable<IMatchResult> GetLastResults(CountryDivision countryDivision, DateTime fromDate)
        {
            return new List<IMatchResult>();
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
                
            _fileService.SaveFile(statusFilePath, new StatusDto
            {
                IsUpdated = false,
                LastCheck = DateTime.Now,
                CreationDateTime = DateTime.Now
            });
        }

        public ICollection<FootballResult> GetHistoricalMatches(string filePath)
        {
            var results = FootballDataCsvParser.GetResults(filePath);

            return results.Select(x => new FootballResult
            {
                DateTime = DateTime.Parse(x.Date),
                HomeTeam = x.HomeTeam,
                AwayTeam = x.AwayTeam,
                HomeScored = int.Parse(string.IsNullOrEmpty(x.FTHG) ? "-1" : x.FTHG),
                AwayScored = int.Parse(string.IsNullOrEmpty(x.FTAG) ? "-1" : x.FTAG),
                HomeOdds = double.Parse(string.IsNullOrEmpty(x.B365H) ? "0.0" : x.B365H),
                DrawOdds = double.Parse(string.IsNullOrEmpty(x.B365D) ? "0.0" : x.B365D),
                AwayOdds = double.Parse(string.IsNullOrEmpty(x.B365A) ? "0.0" : x.B365A)
            }).ToList();
        }

        public ICollection<FootballDataCoUkMatch> GetOriginResults(string filePath)
        {
            return FootballDataCsvParser.GetResults(filePath);
        }

        public ICollection<FootballDataCoUkFixture> GetFixtures(string filePath)
        {
            return FootballDataCsvParser.GetFixture(filePath);
        }
    }
}