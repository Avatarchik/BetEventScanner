using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BetEventScanner.Common.ApiContracts
{
    [DataContract]
    public class TeamContract
    {
        public int Id { get; set; }

        [DataMember(Name = "_links")]
        public LinkContract Links { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "shortName")]
        public string ShortName { get; set; }

        public void GetIdFromUrl()
        {
            var strId = Links.Self.Value.Split(new[] {"/"}, StringSplitOptions.None).Last();

            Id = int.Parse(strId);

            if (Id == 0)
            {
                throw new Exception("TeamEntity Id is zero");
            }
        }
    }
}