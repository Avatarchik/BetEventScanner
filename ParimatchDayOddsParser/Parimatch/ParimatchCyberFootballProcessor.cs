using BetEventScanner.Providers.FifaonlinecupOrg;
using BetEventScanner.Providers.Parimatch;
using BetEventScanner.Providers.Parimatch.Models.CyberFootball;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParimatchDayOddsParser
{
    public class EsportBattleHeadToHead : IHeadToHeadProvider
    {
        public HeadToHead GetHeadToHead(string t1, string t2)
        {
            return new HeadToHead();
        }
    }

    public class PmEsportBattleFootballProcessor : IProcessor
    {
        private readonly CyberFootballBetsProcessor betsProcessor;

        public PmEsportBattleFootballProcessor(CyberFootballBetsProcessor betsProcessor)
        {
            this.betsProcessor = betsProcessor;
        }

        public void Process()
        {
            var matches = GetMatches();

            foreach (var m in matches)
            {
                betsProcessor.AddSnapshot(m);
            }
        }

        private static CyberFootballMatch[] GetMatches()
        {
            var container = ParimatchWebBrowser.GetElementByCssSelector(ParimatchConverter.LiveUrl, "div.container > div.wrapper");
            var liveEvents = ParimatchConverter.GetListLiveEvents2(container);
            var liveMatches = liveEvents.Where(x => x.Competition.ToLower().Contains("cyberfootball. fifa. esports battle.")).ToList();
            if (liveMatches.Count == 0)
            {
                Console.WriteLine("No matches");
                return new CyberFootballMatch[0];
            }

            return liveMatches.Select(CyberFootballMatch.FromLiveMatch).Where(q => q != null).ToArray();
        }
    }

    public class PmCyberStarFootballProcessor : IProcessor
    {
        private readonly IHeadToHeadProvider _headToHeadProvider;
        private readonly CyberFootballBetsProcessor _betsProcessor;

        public PmCyberStarFootballProcessor(IHeadToHeadProvider headToHeadProvider, CyberFootballBetsProcessor betsProcessor)
        {
            _headToHeadProvider = headToHeadProvider;
            _betsProcessor = betsProcessor;
        }

        public void Process()
        {
            var matches = GetMatches();
            if (!matches.Any())
                return;

            //var cache = new Dictionary<string, string>();

            foreach (var lbe in matches)
            {
                _betsProcessor.AddSnapshot(lbe);

                var key = CyberFootballMatch.Key(lbe);
                //if (cache.ContainsKey(key))
                //    continue;

                //cache.Add(key, key);

                var h2h = _headToHeadProvider.GetHeadToHead(lbe.Player1.Name, lbe.Player2.Name);
                var h2hStatistics = CalculateHead2Head(h2h);

                ProcessBets(GetBets(lbe, h2hStatistics));

                Console.WriteLine($"Match:({lbe.Player1.Name}/{lbe.Player1.Team}) - ({lbe.Player2.Name}/{lbe.Player2.Team})");
                Console.WriteLine($"P1:{h2hStatistics.P1WinsCount} - D:{h2hStatistics.DrawsCount} - P2:{h2hStatistics.P2WinsCount}");
            }
        }

        private static CyberFootballMatch[] GetMatches()
        {
            var container = ParimatchWebBrowser.GetElementByCssSelector(ParimatchConverter.LiveUrl, "div.container > div.wrapper");
            var liveEvents = ParimatchConverter.GetListLiveEvents2(container);
            var liveMatches = liveEvents.Where(x => x.Competition.ToLower().Contains("cyberfootball. fifa. cyber stars league.")).ToList();
            if (liveMatches.Count == 0)
            {
                Console.WriteLine("No matches");
                return new CyberFootballMatch[0];
            }

            return liveMatches.Select(CyberFootballMatch.FromLiveMatch).Where(q => q != null).ToArray();
        }

        private static CyberFootballBet[] GetBets(CyberFootballMatch match, HeadToHeadStats stats)
        {
            var bets = new List<CyberFootballBet>();
            var lastWin = stats.LastResult;
            if (lastWin == "x")
                return new CyberFootballBet[0];

            if (match.Player1.Name == lastWin)
            {
                bets.Add(new CyberFootballBet
                {
                    Match = match,
                    Amount = 3,
                    Bet = "win" + match.Player2.Name,
                    BetKey = match.LiveMatch.Win2betKey,
                    Odds = match.LiveMatch.Win2Odds
                });
            }
            else
            {
                bets.Add(new CyberFootballBet
                {
                    Match = match,
                    Amount = 3,
                    Bet = "win" + match.Player1.Name,
                    BetKey = match.LiveMatch.Win1betKey,
                    Odds = match.LiveMatch.Win1Odds
                });
            }

            bets.Add(new CyberFootballBet
            {
                Match = match,
                Amount = 3,
                Bet = "draw",
                BetKey = match.LiveMatch.DrawbetKey,
                Odds = match.LiveMatch.DrawbetKey
            });

            return bets.ToArray();
        }

        private void ProcessBets(CyberFootballBet[] bets)
        {
            foreach (var b in bets)
            {
                if (!_betsProcessor.BetExists(b.EvNo))
                    _betsProcessor.AddBet(b);
            }
        }

        private static HeadToHeadStats CalculateHead2Head(HeadToHead h2h, int take = 20)
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

            var lastMatch = h2h.Results.Last();
            var lastRes = lastMatch.FT.Split(':').Select(int.Parse).ToList();
            if (lastRes[0] == lastRes[1])
            {
                r.LastResult = "x";
            }
            else
            {
                if (lastRes[0] > lastRes[1])
                {
                    r.LastResult = lastMatch.Player1.Name;
                }
                else
                {
                    r.LastResult = lastMatch.Player2.Name;
                }
            }

            return r;
        }
    }
}
