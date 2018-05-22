using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BetEventScanner.Common.Services.FootballDataOrg.Model
{
    [DataContract]
    public class FixturesContract
    {
        [DataMember(Name = "fixtures")]
        public IEnumerable<FixtureContract> Fixtures { get; set; }
    }
}