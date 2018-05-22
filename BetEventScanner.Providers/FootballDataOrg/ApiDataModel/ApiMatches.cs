using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BetEventScanner.Providers.FootballDataOrg.ApiDataModel
{
    [DataContract]
    public class ApiMatches
    {
        [DataMember(Name = "_links")]
        public ApiLinks ApiLinks { get; set; }

        [DataMember(Name = "count")]
        public string Count { get; set; }

        [DataMember(Name = "fixtures")]
        public List<ApiMatch> Matches { get; set; }
    }
}
