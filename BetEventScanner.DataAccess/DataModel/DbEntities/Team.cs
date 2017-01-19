using BetEventScanner.DataAccess.Contracts;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class Team : IEntity
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string ShortName { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }
    }
}