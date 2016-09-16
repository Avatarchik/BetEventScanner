using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BetEventScanner.Common.ApiContracts
{
    [DataContract]
    public class FixturesContract
    {
        [DataMember(Name = "fixtures")]
        public IEnumerable<FixtureContract> Fixtures { get; set; }
    }
}