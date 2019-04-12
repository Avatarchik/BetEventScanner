using System.Linq;

namespace BetEventScanner.DataModel
{
    public class BasketballPeriod
    {
        public int Total { get; set; }
        public string Score { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }

        public static BasketballPeriod FromString(string r)
        {
            var p = new BasketballPeriod
            {
                Score = r
            };

            var scores = r.Split(':').Select(int.Parse).ToArray();
            p.Total = scores.Sum();
            p.Score1 = scores[0];
            p.Score2 = scores[1];

            return p;
        }
    }
}
