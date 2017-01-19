using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;
using BetEventScanner.DesktopUI.Annotations;
using CsvHelper;

namespace BetEventScanner.Common.ResultsService
{
    public class FootballDataCsvParser : IResultsService
    {
        private static readonly Dictionary<string, string> Replacements = new Dictionary<string, string>()
        {
            {"BbMx>2.5", "BbMx_more_25"},
            {"BbAv>2.5", "BbAv_more_25"},
            {"BbMx<2.5", "BbMx_less_25"},
            {"BbAv<2.5", "BbAv_less_25"}
        };

        public IEnumerable<IMatchResult> GetResults(string filePath)
        {
            AdaptHeaderRow(filePath);

            return ParseResultsData(filePath);
        }

        private void AdaptHeaderRow(string fileName)
        {
            var sourceLines = File.ReadAllLines(fileName);

            var headerLine = sourceLines.First();

            foreach (var replacement in Replacements)
            {
                headerLine = headerLine.Replace(replacement.Key, replacement.Value);
            }

            sourceLines[0] = headerLine;

            File.WriteAllLines(fileName, sourceLines);
        }

        private IEnumerable<IMatchResult> ParseResultsData(string fileName)
        {
            var result = new List<IMatchResult>();
            using (var reader = new StreamReader(fileName, Encoding.UTF8))
            {
                var csvReader = new CsvReader(reader);
                csvReader.Configuration.HasHeaderRecord = true;
                //csvReader.Configuration.RegisterClassMap<FootballDataResultMapping>();

                while (csvReader.Read())
                {
                    var matchResult = csvReader.GetRecord<FootaballDataResultDto>();
                    result.Add(ConvertResult(matchResult));
                }
            }

            return result;
        }

        private static IMatchResult ConvertResult(FootaballDataResultDto dto)
        {
            return new MathcMatchResult
            {
                HomeTeam = new FootballTeam(dto.HomeTeam, "0", dto.HomeTeam),
                AwayTeam = new FootballTeam(dto.AwayTeam, "0", dto.AwayTeam),
                HomeTeamScored = int.Parse(dto.FTHG),
                AwayTeamScored = int.Parse(dto.FTAG),
                Result = $"{dto.FTHG} - {dto.FTAG}"
            };
        }
    }

    public class FootballDataService
    {
        public readonly string Url = "http://www.football-data.co.uk/";
        FootballDataCsvParser _parser = new FootballDataCsvParser();
        private IFileDataSource _dataSource = new DataSourceFootballData();
        private IFileService _fileService;
        private IEnumerable<string> _supportedLeagues = new List<string> {"E0", "E1", "E2", "E3", "E1", "EC" };

        public void ProcessData()
        {
            var startYear = "93";
            var endYear = "94";

            var downloadedFileName = $"mmz4281/9293/EC.csv";
            var targetUrl = $"{Url}{downloadedFileName}";
            _dataSource.DownloadFile(targetUrl, "temp123.csv");

        }
    }

    public class DataSourceFootballData : IFileDataSource
    {
        private string fileName = "mmz4281/1617/E0.csv";

        public bool DownloadFile(string url, string pathToStore)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(url + fileName, "test123.csv");
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public interface IFileDataSource
    {
        bool DownloadFile(string url, string pathToStore);
    }

    public interface IFileService
    {
        void MoveFile(string from, string to);
    }
}
