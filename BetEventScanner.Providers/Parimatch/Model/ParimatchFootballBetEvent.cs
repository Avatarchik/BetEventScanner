using System;

namespace BetEventScanner.Providers.Parimatch.Model
{
    public class ParimatchFootballBetEvent : BetEventBase, IParimatchEvent
    {
        public string ParimatchId { get; set; }

        public string MatchId { get; set; }

        public DateTime DateTime { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string HomeHandicap { get; set; }

        public string HomeHandicapOdds { get; set; }

        public string AwayHandicap { get; set; }

        public string AwayHandicapOdds { get; set; }

        public string HomeWin { get; set; }

        public string Draw { get; set; }

        public string AwayWin { get; set; }

        public string HomeWinOrDraw { get; set; }

        public string NoDraw { get; set; }

        public string AwayWinOrDraw { get; set; }

        public string IndTotalHome { get; set; }

        public string IndTotalHomeOver { get; set; }

        public string IndTotalHomeUnder { get; set; }

        public string IndTotalAway { get; set; }

        public string IndTotalAwayOver { get; set; }

        public string IndTotalAwayUnder { get; set; }

        public string FirstHalfScore { get; set; }

        public string FinalScore { get; set; }

        public string ResultStatus { get; set; }
    }
}