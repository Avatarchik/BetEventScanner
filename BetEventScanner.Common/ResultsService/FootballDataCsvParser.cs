using System.Collections.Generic;
using System.IO;
using System.Text;
using BetEventScanner.Common.Services.FootbalDataCoUk;
using CsvHelper;
using CsvHelper.Configuration;

namespace BetEventScanner.Common.ResultsService
{
    public static class FootballDataCsvParser
    {
        public static ICollection<FootballDataCoUkMatch> GetResults(string filePath)
        {
            return Parse<FootballDataCoUkMatch>(filePath);
        }

        public static ICollection<FootballDataCoUkFixture> GetFixture(string filePath)
        {
            return Parse<FootballDataCoUkFixture>(filePath, new FootballDataCoUkFixtureMapping());
        }

        private static ICollection<T> Parse<T>(string filePath, CsvClassMap classMap  = null)
        {
            var result = new List<T>();

            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            {
                var csvReader = new CsvReader(reader);
                csvReader.Configuration.HasHeaderRecord = true;

                if (classMap != null)
                {
                    csvReader.Configuration.RegisterClassMap(classMap);    
                }

                while (csvReader.Read())
                {
                    var matchResult = csvReader.GetRecord<T>();
                    result.Add(matchResult);
                }
            }

            return result;
        }
    }
}
