using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BetEventScanner.Common.ApiDataModel
{
    //[DataContract]
    [CollectionDataContract]
    public class SeasonCompetitionsContract : List<CompetitionContract>
    {
        //public CompetitionContract[] Competitions { get; set; }
    }
}