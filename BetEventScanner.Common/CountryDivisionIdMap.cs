using System.Collections.Generic;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common
{
    public static class CountryDivisionIdMap
    {
        public static IDictionary<CountryDivision, int> Map { get; } = new Dictionary<CountryDivision, int>
        {
            { CountryDivision.Germany1, 394 },
            { CountryDivision.Germany2, 395 }
        };
    }
}