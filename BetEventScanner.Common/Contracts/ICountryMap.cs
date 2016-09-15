using System.Collections.Generic;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common.Contracts
{
    public interface ICountryMap
    {
        IDictionary<CountryDivision, int> Map { get; }

        CountryDivision GetCompetitionById(int divisionId);
    }
}