using System.Runtime.Serialization;

namespace BetEventScanner.Common.ApiDataModel
{
    [DataContract]
    public class SeasonCompetitionsContract
    {
        [DataMember(Order = 0)]
        public CompetitionContract[] Competitions { get; set; }
    }
}