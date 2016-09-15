using System.Runtime.Serialization;
using BetEventScanner.DataAccess.DataModel.Entities;

namespace BetEventScanner.Common.ApiContracts
{
    // ToDo Remove it
    [DataContract]
    public class StandingContract
    {
        [DataMember(Name = "APIVersion")]
        public string APIVersion { get; set; }

        [DataMember(Name = "APIRequestsRemaining")]
        public int APIRequestsRemaining { get; set; }

        [DataMember(Name = "DeveloperAuthentication")]
        public string DeveloperAuthentication { get; set; }

        [DataMember(Name = "teams")]
        public StandinfTeam[] Teams { get; set; }
    }
}
