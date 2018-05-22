using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BetEventScanner.DataModel;
using BetEventScanner.DataModel.Model;

namespace BetEventScanner.Common.Services
{
    public class FootballHistoricalStatistics
    {
        private readonly IDictionary<string, int> _statiscs = new Dictionary<string, int>();

        private readonly IDictionary<string, Func<FootballMatchResult, bool>> _scoreRules = new Dictionary
            <string, Func<FootballMatchResult, bool>>
        {
            //{"0-0", mr => mr.HomeScored == 0 && mr.AwayScored == 0},
            //{"1-0", mr => mr.HomeScored == 1 && mr.AwayScored == 0},
            //{"2-0", mr => mr.HomeScored == 2 && mr.AwayScored == 0},
            //{"3-0", mr => mr.HomeScored == 3 && mr.AwayScored == 0},
            //{"0-1", mr => mr.HomeScored == 0 && mr.AwayScored == 1},
            //{"0-2", mr => mr.HomeScored == 0 && mr.AwayScored == 2},
            //{"0-3", mr => mr.HomeScored == 0 && mr.AwayScored == 3},
            //{"1-1", mr => mr.HomeScored == 1 && mr.AwayScored == 1},
            //{"2-1", mr => mr.HomeScored == 2 && mr.AwayScored == 1},
            //{"3-1", mr => mr.HomeScored == 3 && mr.AwayScored == 1},
            //{"1-2", mr => mr.HomeScored == 1 && mr.AwayScored == 2},
            //{"1-3", mr => mr.HomeScored == 1 && mr.AwayScored == 3},
            //{"2-2", mr => mr.HomeScored == 2 && mr.AwayScored == 2},
            //{"3-2", mr => mr.HomeScored == 3 && mr.AwayScored == 2},
            //{"2-3", mr => mr.HomeScored == 2 && mr.AwayScored == 3},
            //{"3-3", mr => mr.HomeScored == 3 && mr.AwayScored == 3}
        };

        public void ProcessMatch(FootballMatchResult matchMatchResult)
        {
            foreach (var rule in _scoreRules.Where(rule => rule.Value.Invoke(matchMatchResult)))
            {
                if (!_statiscs.ContainsKey(rule.Key))
                {
                    _statiscs.Add(rule.Key, 1);
                }
                else
                {
                    _statiscs[rule.Key]++;
                }
            }
        }

        public string GetSummary()
        {
            var sb = new StringBuilder();

            foreach (var source in _statiscs.OrderByDescending(x => x.Value))
            {
                sb.AppendLine($"{source.Key} : {source.Value}");
            }

            return sb.ToString();
        }

        public void ProcessMatches(IEnumerable<FootballMatchResult> matchResults)
        {
            foreach (var matchResult in matchResults)
            {
                ProcessMatch(matchResult);
            }
        }
    }
}