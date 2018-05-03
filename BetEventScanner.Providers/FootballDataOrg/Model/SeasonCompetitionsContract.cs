using System.Collections.Generic;
using System.Runtime.Serialization;
using BetEventScanner.Common.Services.FootballDataOrg.Model;
using BetEventScanner.Providers.FootballDataOrg.ApiDataModel;

namespace BetEventScanner.Providers.FootballDataOrg.Model
{
    [CollectionDataContract]
    public class SeasonCompetitionsContract : List<CompetitionContract>
    {
    }
}