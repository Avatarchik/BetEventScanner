using System.Runtime.Serialization;

namespace BetEventScanner.DataAccess.DataModel.DbEntities.Old
{
    [DataContract]
    public class SchedulerAndResults
    {
        [DataMember(Name = "APIVersion")]
        public string ApiVersion { get; set; }

        [DataMember(Name = "APIRequestsRemaining")]
        public string ApiRequestsRemaining { get; set; }

        [DataMember(Name = "DeveloperAuthentication")]
        public string DeveloperAuthentication { get; set; }

        [DataMember(Name = "matches")]
        public Match[] Matches { get; set; }
    }
}
