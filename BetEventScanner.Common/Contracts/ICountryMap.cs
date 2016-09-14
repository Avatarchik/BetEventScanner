using System.Collections.Generic;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common.Contracts
{
    public interface ICountryMap
    {
        IDictionary<CountryDivisionEnum, int> Map { get; }
    }
}