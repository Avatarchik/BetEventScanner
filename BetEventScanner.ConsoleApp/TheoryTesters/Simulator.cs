using System;
using System.Collections.Generic;
using BetEventScanner.Common.Services.FootbalDataCoUk.Model;
using BetEventScanner.ConsoleApp.TheoryStrateges.Equalizer;
using BetEventScanner.DataModel.Model;

namespace BetEventScanner.ConsoleApp.TheoryTesters
{
    public class Simulator
    {
        public SimulationResult Simulate(IEnumerable<HistoricalMatch> matchResults)
        {
            var simulationResult = new SimulationResult();
            var count = 0;
            var totalCount = 0;

            var equalizer = new EqualizerService();
            //var stats = new FootballHistoricalStatistics();

            foreach (var matchResult in matchResults)
            {
                var oddsH1 = double.Parse(matchResult.B365H);
                var oddsH2 = double.Parse(matchResult.B365A);

                totalCount++;

                if (oddsH1 < 2.0 || oddsH2 < 2.0)
                {
                    Console.WriteLine($"N{totalCount}; Home:{matchResult.B365H}; Away:{matchResult.B365A}; skipped");
                    continue;
                }

                count++;

                //var oddsMock = GetOddsMock(oddsH1, oddsD, oddsH2);
                //if (oddsMock == null) continue;

                var homeScored = int.Parse(matchResult.FTHG);
                var awayScored = int.Parse(matchResult.FTAG);

                Console.WriteLine(
                    $"N{totalCount}/{count}; {matchResult.HomeTeam}({matchResult.B365H}) {homeScored} - {awayScored} {matchResult.AwayTeam}({matchResult.B365A})");

                if (homeScored + awayScored <= 1)
                {
                    simulationResult.TotalUnder15Count++;
                }

                if (homeScored + awayScored >= 4)
                {
                    simulationResult.TotalOver35Count++;
                }

                if (homeScored == 1 && awayScored == 1)
                {
                    simulationResult.Draw11Count++;
                }

                if (homeScored == 2 && awayScored == 1)
                {
                    simulationResult.H1Win21Count++;
                }

                if (homeScored == 1 && awayScored == 2)
                {
                    simulationResult.H2Win12Count++;
                }

                if (homeScored - awayScored >= 2)
                {
                    simulationResult.HandicapH1Minus15Count++;
                }

                if (awayScored - homeScored >= 2)
                {
                    simulationResult.HandicapH2Minus15Count++;
                }

                if (homeScored == 0 || awayScored == 0)
                {
                    simulationResult.EvenOneNotScoredCount++;
                }

                var footballMatchResult = new FootballMatchResult
                {
                    FinalHomeScored = homeScored,
                    FinalAwayScored = awayScored
                };

                equalizer.Play(footballMatchResult);

                //stats.ProcessMatch(footballMatchResult);

                Console.WriteLine($"Under15: {equalizer.EqualizerState.Under15}");
                Console.WriteLine($"Over35: {equalizer.EqualizerState.Over35}");
                Console.WriteLine($"EvenOneNotScore: {equalizer.EqualizerState.EvenOneScore}");
                Console.WriteLine($"DirectScore: {equalizer.EqualizerState.DirectScore}");
                Console.WriteLine($"Handicap: {equalizer.EqualizerState.Handicap}");

                Console.ReadLine();
            }

            Console.WriteLine($"Total matches played by equal Teams: {count}");
            Console.WriteLine($"Under 1.5 count: {simulationResult.TotalUnder15Count} MaxTransactionLength: {simulationResult.TotalUnder15Transaction}");
            Console.WriteLine($"Over 3.5 count: {simulationResult.TotalOver35Count}");
            Console.WriteLine($"1-1 count: {simulationResult.Draw11Count}");
            Console.WriteLine($"2-1 count: {simulationResult.H1Win21Count}");
            Console.WriteLine($"1-2 count: {simulationResult.H2Win12Count}");
            Console.WriteLine($"Home -1.5 count: {simulationResult.HandicapH1Minus15Count}");
            Console.WriteLine($"Away -1.5 count: {simulationResult.HandicapH2Minus15Count}");
            Console.WriteLine($"EvenOneNotScored count: {simulationResult.EvenOneNotScoredCount}");

            return simulationResult;
        }      
    }
}