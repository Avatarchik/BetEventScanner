using System;
using BetEventScanner.Common.ApiContracts;
using BetEventScanner.DataAccess.DataModel.DbEntities;

namespace BetEventScanner.Common
{
    public static class Converters
    {
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
    }
}
