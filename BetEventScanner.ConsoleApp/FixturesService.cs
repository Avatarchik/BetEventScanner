using System.Collections.Generic;
using System.IO;
using System.Linq;
using BetEventScanner.Common.Services.FootbalDataCoUk;
using BetEventScanner.DataAccess.EF;
using BetEventScanner.Providers.FootballDataCoUk;
using FootballMatchResult = BetEventScanner.DataModel.Model.FootballMatchResult;

namespace BetEventScanner.ConsoleApp
{
    public class FixturesService
    {
        public void UpdateIncomingMatches()
        {
            // ToDo move to settings
            var fixturesBaseDir = new DirectoryInfo(@"C:\BetEventScanner\Services\FootballDataCoUk\fixtures");
            var fixturesFiles = fixturesBaseDir.GetFiles().OrderBy(x => x.CreationTime);

            var fixtures = new FootballDataCoUkService().GetFixtures(fixturesFiles.First().FullName);

            //var newFixtures = GetNewFixtures(fixtures);
            //
            //var incomingMatches = newFixtures.Select(x => new IncomingMatch
            //{
            //    //Div = x.Div,
            //    //DateTime = x.DateTime,
            //    //HomeTeam = x.HomeTeam,
            //    //AwayTeam = x.AwayTeam,
            //    //HomeOdds = x.HomeOdds,
            //    //DrawOdds = x.DrawOdds,
            //    //AwayOdds = x.AwayOdds,
            //    //Over25Odds = x.Over25Odds,
            //    //Under25Odds = x.Under25Odds
            //});

            //using (var context = new BetEventScannerContext())
            //{
            //    context.IncomingMatches.AddRange(incomingMatches);
            //    context.SaveChanges();
            //}
        }

        private ICollection<FootballMatchResult> GetNewFixtures(ICollection<FootballMatchResult> fixtures)
        {
            return fixtures;
        }
    }
}