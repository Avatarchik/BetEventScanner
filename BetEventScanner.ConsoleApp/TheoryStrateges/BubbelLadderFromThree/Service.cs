using System;
using System.Collections.Generic;

namespace BetEventScanner.ConsoleApp.TheoryStrateges.BubbelLadderFromThree
{
    public class Setting
    {
        public decimal MinBetValue { get; set; } = 3.0m;

        public decimal MinGrowthValue { get; set; } = 3.0m;
    }

    public class Calculator
    {
        private readonly Setting _setting;

        public Calculator()
        {
            _setting = new Setting();
        }

        public decimal ClaculateBetValue(decimal lost, decimal betRateValue)
        {
            var i = 1;

            decimal betValue;

            while (true)
            {
                betValue = (i + _setting.MinGrowthValue) / (betRateValue - 1.0m);
                var resultSum = betValue * betRateValue;
                if (resultSum > (lost + betValue))
                {
                    break;
                }

                i++;
            }

            return betValue;
        }
    }

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
