using System;
using System.IO;
using System.Linq;
using BetEventScanner.Common;
using BetEventScanner.Common.Services.FootbalDataCoUk;
using BetEventScanner.DataModel;
using FootballHistoricalStatistics = BetEventScanner.DataModel.FootballHistoricalStatistics;

namespace BetEventScanner.ConsoleApp
{
    public static class HistoricalStatisticsProcessor
    {
        public static void ProcessCsvFiles(IHistoricalResultsDataSource dataSource, Func<FootballMatchResult, bool> filter)
        {
            var files = Directory.GetFiles(@"C:\BetEventScanner\Services\FootballDataCoUk\Data");

            foreach (var file in files)
            {
                var sourceResults = dataSource.GetHistoricalMatches(file);
                 
                var filteredResults = sourceResults.Where(filter).ToList();

                //foreach (var res in filteredResults)
                //{
                //    if (res.AwayOdds > res.HomeOdds && res.AwayScored < res.HomeScored)
                //    {
                //        Console.WriteLine($"{res.HomeTeam}({res.HomeOdds}) {res.HomeScored}-{res.AwayScored} ({res.AwayOdds}){res.AwayTeam}");
                //    }
                //}

                //Console.ReadLine();

                var statistics = new FootballHistoricalStatistics();
                statistics.ProcessMatches(filteredResults);

                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine(file);
                Console.WriteLine($"Total:{sourceResults.Count}; Filtered:{sourceResults.Count - filteredResults.Count}");
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine(statistics.GetSummary());
                Console.WriteLine("-----------------------------------------------------------------");
            }
        }
    }
}