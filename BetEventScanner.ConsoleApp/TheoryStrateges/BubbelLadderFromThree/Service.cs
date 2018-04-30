using System;

namespace BetEventScanner.ConsoleApp.TheoryStrateges.BubbelLadderFromThree
{
    public class TennisMatch
    {
        public TennisTournament TennisTournament { get; set; }

        public TennisPlayer TennisPlayer1 { get; set; }

        public TennisPlayer TennisPlayer2 { get; set; }

        public TennisMatchOdds TennisMatchOdds { get; set; }
    }

    public class TennisTournament
    {
        public string Name { get; set; }
    }

    public class TennisPlayer
    {
        public string Name { get; set; }

        public int Rank { get; set; }

        public DateTime Updated { get; set; }
    }

    public class TennisMatchOdds
    {
        public decimal Player1Win { get; set; }

        public decimal Player2Win { get; set; }

        public decimal SK1Value { get; set; }

        public string SK1Description { get; set; }

        public decimal SK2Value { get; set; }

        public string SK2Description { get; set; }

        public decimal SK3Value { get; set; }

        public string SK3Description { get; set; }
    }
}
