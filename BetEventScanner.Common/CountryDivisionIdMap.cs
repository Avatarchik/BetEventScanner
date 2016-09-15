using System.Collections.Generic;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common
{
    public static class CountryDivisionIdMap
    {
        public static IDictionary<CountryDivisionEnum, int> Map { get; } = new Dictionary<CountryDivisionEnum, int>
        {
            { CountryDivisionEnum.Germany1, 394 },
            { CountryDivisionEnum.Germany2, 395 }
        };
    }
}