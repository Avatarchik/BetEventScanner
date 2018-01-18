using System.Collections.Generic;
using System.Runtime.Serialization;
using BetEventScanner.Common.Services.FootballDataOrg.Model;

namespace BetEventScanner.Providers.FootballDataOrg.Model
{
    [CollectionDataContract]
    public class SeasonCompetitionsContract : List<CompetitionContract>
    {
    }
}