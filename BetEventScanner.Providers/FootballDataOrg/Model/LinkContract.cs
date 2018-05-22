using System.Runtime.Serialization;

namespace BetEventScanner.Common.ApiContracts
{
    [DataContract]
    public class LinkContract
    {
        [DataMember(Name = "self")]
        public LinkValueContract Self { get; set; }

        [DataMember(Name = "fixtures")]
        public LinkValueContract Fixtures { get; set; }

        [DataMember(Name = "players")]
        public LinkValueContract Players { get; set; }

    }
}