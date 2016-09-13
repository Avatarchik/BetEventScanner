using BetEventScanner.Common.ApiDataModel;

namespace BetEventScanner.Common.DataModel
{
    public class Team
    {
        public Team(ApiTeam apiTeam)
        {
            
            Name = apiTeam.Name;
            Code = apiTeam.Code;
            ShortName = apiTeam.ShortName;
        }

        public string Name { get; set; }

        public string Code { get; set; }

        public string ShortName { get; set; }

    }
}
