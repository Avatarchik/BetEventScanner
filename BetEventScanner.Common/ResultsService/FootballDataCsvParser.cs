using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.Contracts.Services;
using BetEventScanner.Common.DataModel;
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

    class FootballDataCsvParserImpl : FootballDataCsvParser
    {
    }

    public class DataSourceFootballData : IDataSource
    {
        public bool DownloadFile(string url, string pathToStore)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(url, pathToStore);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    public interface IDataSource
    {
        bool DownloadFile(string url, string pathToStore);
    }


}
