using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BetEventScanner.Providers.FootballDataOrg.ApiDataModel
{
    [DataContract]
    public class ApiCountryTeams
    {
        [DataMember(Name = "_links")]
        public ApiLinks ApiLinks { get; set; }

        [DataMember(Name = "teams")]
        public List<ApiTeam> Teams { get; set; }
    }
}
