using System.Runtime.Serialization;

namespace BetEventScanner.DataAccess.DataModel.Entities
{
    [DataContract]
    public class MatchCommentaries
    {  
        [DataMember(Name= "APIVersion")]
        public string ApiVersion { get; set; }

        [DataMember(Name = "APIRequestsRemaining")]
        public string ApiRequestsRemaining { get; set; }

        [DataMember(Name = "DeveloperAuthentication")]
        public string DeveloperAuthentication { get; set; }

        [DataMember(Name= "commentaries")]
        public Commentaries[] Commentaies { get; set; }

    }
}
