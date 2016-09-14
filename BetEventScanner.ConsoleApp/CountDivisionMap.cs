using System.Collections.Generic;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.ConsoleApp
{
    public static class CountDivisionMap
    {
        public static IDictionary<CountryEnum, IEnumerable<CountryDivisionEnum>> Map { get; } = new Dictionary<CountryEnum, IEnumerable<CountryDivisionEnum>>
        {
            { CountryEnum.Germany, new List<CountryDivisionEnum> { CountryDivisionEnum.Germany1, CountryDivisionEnum.Germany2 } },
            { CountryEnum.England, new List<CountryDivisionEnum> { CountryDivisionEnum.England1, CountryDivisionEnum.England2 } }  
        };
    }
}