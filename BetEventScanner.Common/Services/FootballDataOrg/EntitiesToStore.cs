using System.Collections.Generic;
using BetEventScanner.DataAccess.DataModel;
using BetEventScanner.DataAccess.DataModel.DbEntities;

namespace BetEventScanner.Common
{
    public class EntitiesToStore
    {
        public List<CountryEnum> Countries { get; } = new List<CountryEnum>();

        public IDictionary<CountryEnum, CountryTeamsDocEntity> CountryTeamsStorage { get; } = new Dictionary<CountryEnum, CountryTeamsDocEntity>();

        public IDictionary<CountryEnum, CountryCompetitionsDocEntity> Competitions { get; } = new Dictionary<CountryEnum, CountryCompetitionsDocEntity>();

        public IDictionary<CountryEnum, CountryCompetitionFixturesDocEntity> Fixtures { get; } = new Dictionary<CountryEnum, CountryCompetitionFixturesDocEntity>();

        public IDictionary<CountryEnum, CountryCompetitionsStatisticsDocEntity> Statistics { get; } = new Dictionary<CountryEnum, CountryCompetitionsStatisticsDocEntity>();

        public void AddCountries(IEnumerable<CountryEnum> countries)
        {
            Countries.AddRange(countries);
        }

        public void AddCountryTeam(CountryEnum countryEnum, IEnumerable<Team> teams)
        {
            var key = countryEnum;

            if (!CountryTeamsStorage.ContainsKey(key))
            {
                var countryTeams = new CountryTeamsDocEntity();
                countryTeams.Teams.AddRange(teams);
                CountryTeamsStorage[key] = countryTeams;
            }
            else
            {
                CountryTeamsStorage[key].Teams.AddRange(teams);
            }
        }

        public void AddCompetition(CountryEnum countryEnum, CompetitionEntity competitionEntity)
        {
            var key = countryEnum;

            if (!Competitions.ContainsKey(key))
            {
                var countryCompetitons = new CountryCompetitionsDocEntity();
                countryCompetitons.Competitions.Add(competitionEntity);
                Competitions.Add(countryEnum, countryCompetitons);
            }
            else
            {
                Competitions[key].Competitions.Add(competitionEntity);
            }
        }

        public void AddFixtures(CountryEnum countryEnum, CompetitionFixturesEntity fixtures)
        {
            var key = countryEnum;

            if (!Fixtures.ContainsKey(key))
            {
                var countryCompetitionFixtures = new CountryCompetitionFixturesDocEntity();
                countryCompetitionFixtures.CompetitionFixtures.Add(fixtures);
                Fixtures.Add(key, countryCompetitionFixtures);
            }
            else
            {
                Fixtures[key].CompetitionFixtures.Add(fixtures);
            }
        }

        public void AddStatistics(CountryEnum countryEnum, CompetitionStatisticsEntity statistics)
        {
            var key = countryEnum;

            if (!Statistics.ContainsKey(key))
            {
                var countryStatistics = new CountryCompetitionsStatisticsDocEntity();
                countryStatistics.Statistics.Add(statistics);
                Statistics.Add(countryEnum, countryStatistics);
            }
            else
            {
                Statistics[key].Statistics.Add(statistics);
            }  
        }
    }
}