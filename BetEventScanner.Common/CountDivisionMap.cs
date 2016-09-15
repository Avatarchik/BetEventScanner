using System.Collections.Generic;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common
{
    public static class CountDivisionMap
    {
        public static IDictionary<CountryEnum, IEnumerable<CountryDivisionEnum>> Map { get; } = new Dictionary<CountryEnum, IEnumerable<CountryDivisionEnum>>
        {
            { CountryEnum.Germany, new List<CountryDivisionEnum> { CountryDivisionEnum.Germany1, CountryDivisionEnum.Germany2 } },
            { CountryEnum.England, new List<CountryDivisionEnum> { CountryDivisionEnum.England1, CountryDivisionEnum.England2 } },  
            { CountryEnum.Italy, new List<CountryDivisionEnum> { CountryDivisionEnum.Italy1 } },  
            { CountryEnum.Spain, new List<CountryDivisionEnum> { CountryDivisionEnum.Spain1, CountryDivisionEnum.Spain2 } },  
        };
    }
}