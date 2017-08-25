using BetEventScanner.DataModel;

namespace BetEventScanner.ConsoleApp
{
    public class EqualizerService
    {
        public EqualizerService()
        {
            EqualizerState = new EqualizerState();
        }

        public EqualizerState EqualizerState { get; }

        public void Play(FootballResult matchResult)
        {
            UpdateState(matchResult);
        }

        private void UpdateState(FootballResult matchResult)
        {
            if (matchResult.HomeScored == 0 || matchResult.AwayScored == 0)
            {
                EqualizerState.EvenOneScore = 1;
            }
            else
            {
                EqualizerState.EvenOneScore++;
            }

            if (matchResult.OverallTotal >= 4)
            {
                EqualizerState.Over35 = 1;
            }
            else
            {
                EqualizerState.Over35++;
            }

            if (matchResult.OverallTotal <= 1)
            {
                EqualizerState.Under15 = 1;
            }
            else
            {
                EqualizerState.Under15++;
            }

            var scoreHited = matchResult.HomeScored == 1 && matchResult.AwayScored == 1 || matchResult.HomeScored == 2 && matchResult.AwayScored == 1 || matchResult.HomeScored == 1 && matchResult.AwayScored == 2;

            if (scoreHited)
            {
                EqualizerState.DirectScore = 1;
            }
            else
            {
                EqualizerState.DirectScore++;
            }

            var handicap = matchResult.HomeScored - matchResult.AwayScored >= 2 || matchResult.AwayScored - matchResult.HomeScored >= 2;

            if (handicap)
            {
                EqualizerState.Handicap = 1;
            }
            else
            {
                EqualizerState.Handicap++;
            }
        }
    }
}