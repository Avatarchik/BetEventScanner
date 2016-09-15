﻿using System.Collections.Generic;
using BetEventScanner.DataAccess.DataModel.Entities;

namespace BetEventScanner.Common
{
    public class EntitiesToStore
    {
        public IDictionary<Country, CountryTeamsEntity> CountryTeamsStorage { get; } = new Dictionary<Country, CountryTeamsEntity>();

        public IDictionary<Country, IList<CompetitionEntity>> Competitions { get; } = new Dictionary<Country, IList<CompetitionEntity>>();

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

    }
}