using System;
using System.Collections.Generic;
using System.IO;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.Contracts.Services;
using BetEventScanner.Common.DataModel;
using BetEventScanner.Common.ResultsService;
using BetEventScanner.Common.Services.FootbalDataCoUk.Dto;

namespace BetEventScanner.Common.Services.FootbalDataCoUk
{
    public class FootballDataCoUkService : IFootballService
    {
        public readonly string Url = "http://www.football-data.co.uk/";
        FootballDataCsvParser _parser = new FootballDataCsvParser();
        private IDataSource _dataSource = new DataSourceFootballData();
        private readonly IFileService _fileService = new FileService();
        private IEnumerable<string> _supportedLeagues = new List<string> {"E0"/*, "E1", "E2", "E3", "E1", "EC"*/ };
        private IResultsService _resultsService;
        private string directory = "c:\\BetEventScanner\\Services\\FootballDataCoUk";
        private string statusFile = "status.xml";

        public string Name { get; } = "FootballDataCoUk";

        public void Init()
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
                    if (_dataSource.DownloadFile(Url + fileName, fileNameToStore))
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
    }
}