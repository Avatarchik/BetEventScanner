using System.Collections.Generic;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common
{
    public class FootballDataCountryMap : ICountryMap
    {
        private static readonly IDictionary<CountryDivisionEnum, int> _map = new Dictionary<CountryDivisionEnum, int>
        {
            { CountryDivisionEnum.England1, 398 },
            { CountryDivisionEnum.England2, 427 },
            { CountryDivisionEnum.Germany1, 394 },
            { CountryDivisionEnum.Germany2, 395 },
            { CountryDivisionEnum.Italy1, 438 },
            { CountryDivisionEnum.Spain1, 436 },
            { CountryDivisionEnum.Spain2, 437 },
        };

        public IDictionary<CountryDivisionEnum, int> Map => _map;
    }
}