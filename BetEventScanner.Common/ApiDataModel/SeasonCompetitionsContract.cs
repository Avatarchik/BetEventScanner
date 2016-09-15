using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BetEventScanner.Common.ApiDataModel
{
    [CollectionDataContract]
    public class SeasonCompetitionsContract : List<CompetitionContract>
    {
    }
}