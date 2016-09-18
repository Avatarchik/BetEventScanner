using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BetEventScanner.Common.ApiContracts
{
    [CollectionDataContract]
    public class SeasonCompetitionsContract : List<CompetitionContract>
    {
    }
}