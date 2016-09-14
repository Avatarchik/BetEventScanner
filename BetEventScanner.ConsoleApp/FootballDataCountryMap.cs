

using System.Collections.Generic;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.ConsoleApp
{
    public class FootballDataCountryMap : ICountryMap
    {
        private static readonly IDictionary<CountryDivisionEnum, int> _map = new Dictionary<CountryDivisionEnum, int>
        {
            { CountryDivisionEnum.Germany1, 394 },
            { CountryDivisionEnum.Germany2, 395 }
        };

        public IDictionary<CountryDivisionEnum, int> Map => _map;
    }
}