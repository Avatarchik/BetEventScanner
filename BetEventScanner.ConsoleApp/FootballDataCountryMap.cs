

using System.Collections.Generic;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.ConsoleApp
{
    public class FootballDataCountryMap : ICountryMap
    {
        private static readonly IDictionary<CountryEnum, int> _map = new Dictionary<CountryEnum, int>
        {
            { CountryEnum.Germany1, 394 },
            { CountryEnum.Germany2, 395 }
        };

        public IDictionary<CountryEnum, int> Map => _map;
    }
}