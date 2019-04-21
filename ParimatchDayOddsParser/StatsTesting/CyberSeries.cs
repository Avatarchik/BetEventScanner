using BetEventScanner.Providers.Parimatch.Models.CyberFootball;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ParimatchDayOddsParser.StatsTesting
{
    public static class CyberSeries
    {
        public static void Test()
        {
            var d1 = new Dictionary<string, CyberPlayerStats>();

            var matches = JsonConvert.DeserializeObject<CyberFootballMatch[]>(File.ReadAllText($@"C:\BetEventScanner\cyberFootball\results\cyberMatches.json"));
            matches.OrderByDescending(x => x.DateTime).Take(2000).Where(q => q.Competition.ToLower().Contains("esports battle")).ToList().ForEach(x =>
            {
                if (!d1.ContainsKey(x.Team1Origin))
                    d1.Add(x.Team1Origin, new CyberPlayerStats
                    {
                        Team = x.Team1Origin
                    });

                if (!d1.ContainsKey(x.Team2Origin))
                    d1.Add(x.Team2Origin, new CyberPlayerStats
                    {
                        Team = x.Team2Origin
                    });

                if (x.Result != null)
                {
                    if (x.Result.MatchHomeScored == x.Result.MatchAwayScored)
                    {
                        d1[x.Team1Origin].AddDraw(x);
                        d1[x.Team2Origin].AddDraw(x);
                    }
                    else if (x.Result.MatchHomeScored > x.Result.MatchAwayScored)
                    {
                        d1[x.Team1Origin].AddWin(x);
                        d1[x.Team2Origin].AddLost(x);
                    }
                    else
                    {
                        d1[x.Team1Origin].AddLost(x);
                        d1[x.Team2Origin].AddWin(x);
                    }
                }

            });

            var bests = d1.Values.OrderByDescending(x => x.LongesWinSerie).Take(20);
            foreach (var best in bests)
            {
                Console.WriteLine($"Best:{best.Team}");
                Console.WriteLine($"Longest win:{best._state.LongesWinSerie}");
                Console.WriteLine($"Longest win:{best._state.LongesDrawSerie}");
                Console.WriteLine($"Longest win:{best._state.LongesLostSerie}");
                Console.Write(new string('-', 20));

                foreach (var item in best._state.LongesWinTeams)
                {
                    Console.WriteLine($"{item.DateTime} {item.Team1Origin} {item.OriginResult} {item.Team2Origin}");
                }

                Console.Write(new string('#', 20));
            }
        }
    }
}
