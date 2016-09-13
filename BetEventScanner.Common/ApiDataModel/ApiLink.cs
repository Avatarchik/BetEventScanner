using System.Runtime.Serialization;

// for football data
namespace BetEventScanner.Common.ApiDataModel
{
    [DataContract]
    public class ApiLink
    {
        [DataMember(Name = "href")]
        public string  Href { get; set; }
    }
}