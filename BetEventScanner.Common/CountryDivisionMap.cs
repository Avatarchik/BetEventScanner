using System.Collections.Generic;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common
{
    public static class CountryDivisionMap
    {
        public static IDictionary<Country, IEnumerable<CountryDivision>> Map { get; } = new Dictionary<Country, IEnumerable<CountryDivision>>
        {
            { Country.Germany, new List<CountryDivision> { CountryDivision.Germany1, CountryDivision.Germany2 } },
            { Country.England, new List<CountryDivision> { CountryDivision.England1, CountryDivision.England2 } },  
            { Country.Italy, new List<CountryDivision> { CountryDivision.Italy1 } },  
            { Country.Spain, new List<CountryDivision> { CountryDivision.Spain1, CountryDivision.Spain2 } },  
        };
    }
}