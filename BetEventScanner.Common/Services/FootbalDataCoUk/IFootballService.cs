using System;
using System.Collections.Generic;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common.Services.FootbalDataCoUk
{
    public interface IFootballService
    {
        string Name { get; }

        void Init();

        IEnumerable<IMatchResult> GetAllResults();

        IEnumerable<IMatchResult> GetLastResults(CountryDivision countryDivision, DateTime fromDate);
    }
}