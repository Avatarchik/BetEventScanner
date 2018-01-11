using BetEventScanner.DataModel;

namespace BetEventScanner.ConsoleApp.TheoryStrateges.Equalizer
{
    public class EqualizerService
    {
        public EqualizerService()
        {
            EqualizerState = new EqualizerState();
        }

        public EqualizerState EqualizerState { get; }

        public void Play(FootballMatchResult matchMatchResult)
        {
            UpdateState(matchMatchResult);
        }

        private void UpdateState(FootballMatchResult matchMatchResult)
        {
            //if (matchMatchResult.HomeScored == 0 || matchMatchResult.AwayScored == 0)
            //{
            //    EqualizerState.EvenOneScore = 1;
            //}
            //else
            //{
            //    EqualizerState.EvenOneScore++;
            //}
            //
            //if (matchMatchResult.OverallTotal >= 4)
            //{
            //    EqualizerState.Over35 = 1;
            //}
            //else
            //{
            //    EqualizerState.Over35++;
            //}
            //
            //if (matchMatchResult.OverallTotal <= 1)
            //{
            //    EqualizerState.Under15 = 1;
            //}
            //else
            //{
            //    EqualizerState.Under15++;
            //}
            //
            //var scoreHited = matchMatchResult.HomeScored == 1 && matchMatchResult.AwayScored == 1 || matchMatchResult.HomeScored == 2 && matchMatchResult.AwayScored == 1 || matchMatchResult.HomeScored == 1 && matchMatchResult.AwayScored == 2;
            //
            //if (scoreHited)
            //{
            //    EqualizerState.DirectScore = 1;
            //}
            //else
            //{
            //    EqualizerState.DirectScore++;
            //}
            //
            //var handicap = matchMatchResult.HomeScored - matchMatchResult.AwayScored >= 2 || matchMatchResult.AwayScored - matchMatchResult.HomeScored >= 2;
            //
            //if (handicap)
            //{
            //    EqualizerState.Handicap = 1;
            //}
            //else
            //{
            //    EqualizerState.Handicap++;
            //}
        }
    }
}