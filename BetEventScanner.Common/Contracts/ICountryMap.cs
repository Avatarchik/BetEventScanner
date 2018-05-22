using System.Collections.Generic;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common.Contracts
{
    public interface ICountryMap
    {
        IDictionary<CountryDivision, int> IdMap { get; }

        CountryDivision GetCompetitionById(int divisionId);

        CountryDivision GetCompetitionByCode(string code);
    }
}