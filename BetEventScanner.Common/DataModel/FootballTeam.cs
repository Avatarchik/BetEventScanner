using BetEventScanner.Common.ApiDataModel;
using BetEventScanner.Common.Contracts;

namespace BetEventScanner.Common.DataModel
{
    public class FootballTeam : ITeam
    {
        public FootballTeam(string name, string code, string shortName)
        {
            Name = name;
            Code = code;
            ShortName = shortName;
        }

        public FootballTeam(ApiTeam apiTeam)
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
