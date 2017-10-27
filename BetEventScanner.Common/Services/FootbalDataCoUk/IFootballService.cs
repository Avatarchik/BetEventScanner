using System;
using System.Collections.Generic;
using BetEventScanner.Common.DataModel;
using BetEventScanner.DataAccess.DataModel;

namespace BetEventScanner.Common.Services.FootbalDataCoUk
{
    public interface IFootballService
    {
        string Name { get; }

        IEnumerable<FootballMatchResult> GetAllResults();

        IEnumerable<FootballMatchResult> GetDivisionResults(CountryDivision countryDivision, DateTime fromDate);
    }
}