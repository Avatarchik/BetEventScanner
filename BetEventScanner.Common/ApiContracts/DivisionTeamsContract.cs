using System.Runtime.Serialization;

namespace BetEventScanner.Common.ApiContracts
{
    [DataContract]
    public class DivisionTeamsContract
    {
        [DataMember(Name = "count")]
        public string Count { get; set; }

        [DataMember(Name = "teams")]
        public TeamContract[] Teams { get; set; }
    }
}