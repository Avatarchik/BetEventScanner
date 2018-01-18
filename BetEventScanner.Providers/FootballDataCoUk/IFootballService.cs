using System;
using System.Collections.Generic;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Providers.FootballDataCoUk
{
    public interface IFootballService
    {
        string Name { get; }

        IEnumerable<FootballMatchResult> GetAllResults();

        IEnumerable<FootballMatchResult> GetDivisionResults(CountryDivision countryDivision, DateTime fromDate);
    }
}