using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace BetEventScanner.DataAccess.DataModel.Entities
{
    [DataContract]
    public class Standing
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
