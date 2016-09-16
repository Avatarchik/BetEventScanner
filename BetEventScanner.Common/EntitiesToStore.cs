using System.Collections.Generic;
using System.Runtime.Serialization;
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

    [DataContract]
    public class OddsContract
    {
        [DataMember(Name = "homeWin")]
        public string HomeWin { get; set; }

        [DataMember(Name = "draw")]
        public string Draw { get; set; }

        [DataMember(Name = "awayWin")]
        public string AwayWin { get; set; }
    }

    [DataContract]
    public class ResultContract
    {
        [DataMember(Name = "goalsHomeTeam")]
        public string GoalsHomeTeam { get; set; }

        [DataMember(Name = "goalsAwayTeam")]
        public string GoalsAwayTeam { get; set; }

    }
}

/*
 * 
 * date: "2016-08-13T11:30:00Z",
status: "FINISHED",
matchday: 1,
homeTeamName: "Hull City FC",
awayTeamName: "Leicester City FC",
result: {
goalsHomeTeam: 2,
goalsAwayTeam: 1
},
odds: {
homeWin: 3.25,
draw: 3.25,
awayWin: 2.2
}
 */
