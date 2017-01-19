using System.Collections.Generic;
using BetEventScanner.DataAccess.DataModel.DbEntities;

namespace BetEventScanner.Common
{
    public class EntitiesToStore
    {
        public List<Country> Countries { get; } = new List<Country>();

        public IDictionary<Country, CountryTeamsEntity> CountryTeamsStorage { get; } = new Dictionary<Country, CountryTeamsEntity>();

        public IDictionary<Country, CountryCompetitionsEntity> Competitions { get; } = new Dictionary<Country, CountryCompetitionsEntity>();

        public IDictionary<Country, CountryCompetitionFixturesEntity> Fixtures { get; } = new Dictionary<Country, CountryCompetitionFixturesEntity>();

        public IDictionary<Country, CountryCompetitionsStatisticsEntity> Statistics { get; } = new Dictionary<Country, CountryCompetitionsStatisticsEntity>();

        public void AddCountries(IEnumerable<Country> countries)
        {
            Countries.AddRange(countries);
        }

        public void AddCountryTeam(Country country, IEnumerable<Team> teams)
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
                var countryCompetitons = new CountryCompetitionsEntity();
                countryCompetitons.Competitions.Add(competitionEntity);
                Competitions.Add(country, countryCompetitons);
            }
            else
            {
                Competitions[key].Competitions.Add(competitionEntity);
            }
        }

        public void AddFixtures(Country country, CompetitionFixturesEntity fixtures)
        {
            var key = country;

            if (!Fixtures.ContainsKey(key))
            {
                var countryCompetitionFixtures = new CountryCompetitionFixturesEntity();
                countryCompetitionFixtures.CompetitionFixtures.Add(fixtures);
                Fixtures.Add(key, countryCompetitionFixtures);
            }
            else
            {
                Fixtures[key].CompetitionFixtures.Add(fixtures);
            }
        }

        public void AddStatistics(Country country, CompetitionStatisticsEntity statistics)
        {
            var key = country;

            if (!Statistics.ContainsKey(key))
            {
                var countryStatistics = new CountryCompetitionsStatisticsEntity();
                countryStatistics.Statistics.Add(statistics);
                Statistics.Add(country, countryStatistics);
            }
            else
            {
                Statistics[key].Statistics.Add(statistics);
            }  
        }
    }
}