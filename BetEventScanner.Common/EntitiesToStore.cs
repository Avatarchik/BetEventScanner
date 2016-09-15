using System;
using System.Collections.Generic;
using System.Linq;
using BetEventScanner.Common.DataModel;
using BetEventScanner.DataAccess.DataModel.Entities;

namespace BetEventScanner.Common
{
    public class EntitiesToStore
    {
        public IDictionary<CountryEnum, CountryTeamsEntity> CountryTeamsStorage { get; } = new Dictionary<CountryEnum, CountryTeamsEntity>();

        public void AddCountryTeam(CountryDivisionEnum countryDivision, IEnumerable<TeamEntity> teams)
        {
            var key = GetCountryNameByDivision(countryDivision);

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

        private static CountryEnum GetCountryNameByDivision(CountryDivisionEnum countryDivision)
        {
            foreach (var map in CountDivisionMap.Map)
            {
                if (map.Value.Contains(countryDivision))
                {
                    return map.Key;
                }
            }

            throw new Exception("Country by division not found");
        }

    }
}