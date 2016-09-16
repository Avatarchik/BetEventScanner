using System.Collections.Generic;
using BetEventScanner.Common.ApiContracts;
using BetEventScanner.DataAccess.DataModel.DbEntities;

namespace BetEventScanner.Common
{
    public class EntitiesToStore
    {
        public IDictionary<Country, CountryTeamsEntity> CountryTeamsStorage { get; } = new Dictionary<Country, CountryTeamsEntity>();

        public IDictionary<Country, IList<CompetitionEntity>> Competitions { get; } = new Dictionary<Country, IList<CompetitionEntity>>();

        public IDictionary<Country, IList<FixturesContract>> Fixtures { get; } = new Dictionary<Country, IList<FixturesContract>>();

        public void AddCountryTeam(Country country, IEnumerable<TeamEntity> teams)
        {
            var key = country;

            if (!CountryTeamsStorage.ContainsKey(key))
            {
                var countryTeams = new CountryTeamsEntity();
                countryTeams.Teams.AddRange(teams);
                CountryTeamsStorage[key] = countryTeams;
            }
            else
            {
                CountryTeamsStorage[key].Teams.AddRange(teams);
            }
        }

        public void AddCompetition(Country country, CompetitionEntity competitionEntity)
        {
            var key = country;

            if (!Competitions.ContainsKey(key))
            {
                Competitions.Add(key, new List<CompetitionEntity> { competitionEntity });
            }
            else
            {
                Competitions[key].Add(competitionEntity);
            }
        }

        public void AddFixtures(Country country, FixturesContract fixtures)
        {
            var key = country;

            if (!Fixtures.ContainsKey(key))
            {
                Fixtures.Add(key, new List<FixturesContract> { fixtures });
            }
            else
            {
                Fixtures[key].Add(fixtures);
            }
        }

    }
}