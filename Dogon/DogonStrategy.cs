namespace Dogon
{
    public static class DogonStrategy
    {
        public static DogonReverseBet CalculateReverseBet(ReverseBet reverseBet, decimal lost)
        {
            return new DogonReverseBet
            {
                Variant1Bet = CalculateDogon(reverseBet.Favorite.Odds, lost),
                Variant2Bet = CalculateDogon(reverseBet.Underdog.Odds, lost),
            };
        }

        public static decimal CalculateDogon(decimal kf, decimal lost)
        {
            if (lost == default(decimal))
            {
                return 3;
            }

            var ps = lost;
            var np = 5.0m;
            var st = (ps + np) / kf - 1;
            return st;
        }
    }
}