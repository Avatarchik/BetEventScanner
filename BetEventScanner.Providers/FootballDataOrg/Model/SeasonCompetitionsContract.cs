using System.Collections.Generic;
using System.Runtime.Serialization;
using BetEventScanner.Providers.FootballDataOrg.ApiDataModel;

namespace BetEventScanner.Providers.FootballDataOrg.Model
{
    [CollectionDataContract]
    public class SeasonCompetitionsContract : List<CompetitionContract>
    {
    }

    public class SeasonCompetitionsContractNew
    {
        public List<CompetitionNew> Competitoins { get; set; }
    }
}