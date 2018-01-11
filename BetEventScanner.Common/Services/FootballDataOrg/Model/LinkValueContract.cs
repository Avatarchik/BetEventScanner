using System.Runtime.Serialization;

namespace BetEventScanner.Common.ApiContracts
{
    [DataContract]
    public class LinkValueContract
    {
        [DataMember(Name = "href")]
        public string Value { get; set; }
    }
}