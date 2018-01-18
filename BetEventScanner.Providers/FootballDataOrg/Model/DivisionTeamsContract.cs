using System.Runtime.Serialization;
using BetEventScanner.Common.ApiContracts;

namespace BetEventScanner.Common.Services.FootballDataOrg.Model
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