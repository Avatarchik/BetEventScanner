using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BetEventScanner.Common.Services.FootballDataOrg.Model
{
    [CollectionDataContract]
    public class SeasonCompetitionsContract : List<CompetitionContract>
    {
    }
}