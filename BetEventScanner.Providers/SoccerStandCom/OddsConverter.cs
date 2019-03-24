namespace BetEventScanner.Providers.SoccerStandCom
{
    public class OddsConverter : IConverter<MatchOdds>
    {
        public ConverterParth Parth { get; } = ConverterParth.Odds;

        public MatchOdds Convert(string html)
        {
            return new MatchOdds();
        }
    }
}