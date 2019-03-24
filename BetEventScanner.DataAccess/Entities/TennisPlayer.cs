using System;

namespace BetEventScanner.DataAccess.Entities
{
    public class TennisPlayer
    {
        public int Id { get; set; }

        public string OriginPlayerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Hand { get; set; }

        public DateTime? BirthDate { get; set; }

        public string СountryCode { get; set; }
    }
}
