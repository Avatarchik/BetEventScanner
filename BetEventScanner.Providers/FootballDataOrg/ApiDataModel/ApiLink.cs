using Newtonsoft.Json;
using System.Runtime.Serialization;

// for football data
namespace BetEventScanner.Providers.FootballDataOrg.ApiDataModel
{
    [DataContract]
    public class ApiLink
    {
        [DataMember(Name = "href")]
        public string  Href { get; set; }
    }
}