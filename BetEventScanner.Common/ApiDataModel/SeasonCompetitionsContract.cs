using System.Collections.Generic;
using System.Runtime.Serialization;
using BetEventScanner.Common.ApiContracts;

namespace BetEventScanner.Common.ApiDataModel
{
    [CollectionDataContract]
    public class SeasonCompetitionsContract : List<CompetitionContract>
    {
    }
}