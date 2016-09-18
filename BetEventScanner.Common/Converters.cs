using System;
using BetEventScanner.Common.ApiContracts;
using BetEventScanner.DataAccess.DataModel.DbEntities;

namespace BetEventScanner.Common
{
    public static class Converters
    {

        public static TeamEntity ToEntity(this TeamContract team)
        {
            return new TeamEntity
            {
                Id = team.Id,
                Code = team.Code,
                ShortName = team.ShortName,
                Name = team.Name
            };
        }

        public static CompetitionEntity ToEntity(this CompetitionContract competition)
        {
            return new CompetitionEntity
            {
                Id = int.Parse(competition.Id),
                Code = competition.Code,
                Name = competition.Name,
                Year = competition.Year,
                CurrentMatchday = int.Parse(competition.CurrentMatchday),
                NumberOfMatchdays = int.Parse(competition.NumberOfMatchdays),
                NumberOfTeams = int.Parse(competition.NumberOfTeams),
                NumberOfGames = int.Parse(competition.NumberOfGames),
                LastUpdated = DateTime.Parse(competition.LastUpdated)
            };
        }

        public static CompetitionFixturesEntity ToEntity(this FixturesContract fixturesContract)
        {
            var result = new CompetitionFixturesEntity();
            foreach (var fixtureContract in fixturesContract.Fixtures)
            {
                result.Fixtures.Add(fixtureContract.ToEntity());
            }
            return result;
        }

        public static FixtureEntity ToEntity(this FixtureContract contract)
        {
            return new FixtureEntity
            {
                Date = DateTime.Parse(contract.Date),
                HomeTeamName = contract.HomeTeamName,
                AwayTeamName = contract.AwayTeamName,
                Matchday = contract.Matchday,
                Status = contract.Status,
                MatchResult = contract.MatchResult.ToEntity(),
                Odds = contract.Odds.ToEntity()
            };
        }

        public static MatchResultEntity ToEntity(this MatchResultContract contract)
        {
            return new MatchResultEntity
            {
                GoalsHomeTeam = contract.GoalsHomeTeam,
                GoalsAwayTeam = contract.GoalsAwayTeam
            };
        }

        public static OddsEntity ToEntity(this OddsContract contract)
        {
            return contract == null ? null :
             new OddsEntity
            {
                HomeWin = contract.HomeWin,
                Draw = contract.Draw,
                AwayWin = contract.AwayWin
            };
        }
    }
}
