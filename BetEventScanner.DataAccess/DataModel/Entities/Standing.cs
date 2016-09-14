using System;
using System.Linq;
using System.Runtime.Serialization;

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

    [DataContract]
    public class DivisionTeams
    {
        [DataMember(Name = "count")]
        public string Count { get; set; }

        [DataMember(Name = "teams")]
        public Team[] Teams { get; set; }
    }

    [DataContract]
    public class Team
    {
        public int Id { get; set; }

        [DataMember(Name = "_links")]
        public Link Links { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "shortName")]
        public string ShortName { get; set; }

        public int GetIdFromUrl()
        {
            var id = 0;

            var strId = Links.Self.Value.Split(new[] {"/"}, StringSplitOptions.None).Last();

            if (id == 0)
            {
                throw new Exception();
            }

            return 1;
        }
    }

    [DataContract]
    public class Link
    {
        [DataMember(Name = "self")]
        public LinkValue Self { get; set; }

        [DataMember(Name = "fixtures")]
        public LinkValue Fixtures { get; set; }

        [DataMember(Name = "players")]
        public LinkValue Players { get; set; }

    }

    [DataContract]
    public class LinkValue
    {
        [DataMember(Name = "href")]
        public string Value { get; set; }
    }
}
