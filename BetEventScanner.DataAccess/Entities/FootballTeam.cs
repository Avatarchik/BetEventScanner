using BetEventScanner.DataAccess.Contracts;

namespace BetEventScanner.DataAccess.DataModel
{
    public class FootballTeam : IEntity
    {
        public int Id { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string ShortName { get; set; }

        public int StadiumId { get; set; }

        public Stadium Stadium { get; set; }
    }
}
