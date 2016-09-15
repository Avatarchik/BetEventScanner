using System.Collections.Generic;
using System.Linq;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common
{
    public class FootballDataCountryMap : ICountryMap
    {
        private static readonly IDictionary<CountryDivision, int> _map = new Dictionary<CountryDivision, int>
        {
            { CountryDivision.England1, 398 },
            { CountryDivision.England2, 427 },
            { CountryDivision.Germany1, 394 },
            { CountryDivision.Germany2, 395 },
            { CountryDivision.Italy1, 438 },
            { CountryDivision.Spain1, 436 },
            { CountryDivision.Spain2, 437 }
        };

        public IDictionary<CountryDivision, int> Map => _map;

        public CountryDivision GetCompetitionById(int divisionId)
        {
            return _map.FirstOrDefault(x => x.Value.Equals(divisionId)).Key;
        }
    }
}