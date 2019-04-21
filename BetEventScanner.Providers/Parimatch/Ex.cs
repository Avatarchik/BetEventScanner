using BetEventScanner.DataModel;

namespace BetEventScanner.Providers.Parimatch
{

    public static class Ex
    {
        public static SportType? ToSportType(this string sportType)
        {
            switch (sportType.ToLower())
            {
                case "football": return SportType.Football;
                case "basketball": return SportType.Basketball;
                case "tennis": return SportType.Tennis;
                default:
                    return null;
            }
        }
    }
}
