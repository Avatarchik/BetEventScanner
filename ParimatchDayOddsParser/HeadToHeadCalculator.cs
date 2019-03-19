using BetEventScanner.Providers.FifaonlinecupOrg;
using System.Linq;

namespace ParimatchDayOddsParser
{
    public static class HeadToHeadCalculator
    {
        public static HeadToHeadStats CalculateHead2Head(HeadToHead h2h, int take = 20)
        {
            var r = new HeadToHeadStats
            {
                Player1 = h2h.Player1,
                Player2 = h2h.Player2
            };

            if (h2h.Results == null)
                return r;

            foreach (var q in h2h.Results.Where(v => v.Status != "cancelled").OrderByDescending(x => x.Date).Take(take))
            {
                var p1 = q.Player1.Name;
                var p2 = q.Player2.Name;
                var result = q.FT.Split(':').Select(int.Parse).ToList();

                if (result[0] == result[1])
                {
                    r.DrawsCount++;
                    continue;
                }

                if (result[0] > result[1])
                {
                    if (p1 == r.Player1)
                    {
                        r.P1WinsCount++;
                    }
                    else
                    {
                        r.P2WinsCount++;
                    }
                    continue;
                }

                if (result[0] < result[1])
                {
                    if (p2 == r.Player2)
                    {
                        r.P2WinsCount++;
                    }
                    else
                    {
                        r.P1WinsCount++;
                    }
                }
            }

            return r;
        }
    }
}
