using System.Collections.Generic;

namespace BetEventScanner.Providers.Parimatch.Models.CyberFootball
{
    public class CyberPlayerStats
    {
        public string Team { get; set; }

        public int LongesWinSerie => LongesWinTeams.Length;
        public int LongesDrawSerie => LongesDrawTeams.Length;
        public int LongesLostSerie => LongesLostTeams.Length;

        public List<CyberFootballMatch> CurrentWinTeams { get; set; } = new List<CyberFootballMatch>();
        public List<CyberFootballMatch> CurrentDrawTeams { get; set; } = new List<CyberFootballMatch>();
        public List<CyberFootballMatch> CurrentLostTeams { get; set; } = new List<CyberFootballMatch>();

        public CyberFootballMatch[] LongesWinTeams { get; set; } = new CyberFootballMatch[0];
        public CyberFootballMatch[] LongesDrawTeams { get; set; } = new CyberFootballMatch[0];
        public CyberFootballMatch[] LongesLostTeams { get; set; } = new CyberFootballMatch[0];


        public void AddWin(CyberFootballMatch t)
        {
            CurrentWinTeams.Add(t);
            CurrentDrawTeams.Clear();
            CurrentLostTeams.Clear();

            if (CurrentWinTeams.Count > LongesWinTeams.Length)
                LongesWinTeams = CurrentWinTeams.ToArray();
        }

        public void AddDraw(CyberFootballMatch t)
        {
            CurrentWinTeams.Clear();
            CurrentDrawTeams.Add(t);
            CurrentLostTeams.Clear();

            if (CurrentDrawTeams.Count > LongesDrawTeams.Length)
                LongesDrawTeams = CurrentDrawTeams.ToArray();
        }

        public void AddLost(CyberFootballMatch t)
        {
            CurrentWinTeams.Clear();
            CurrentDrawTeams.Clear();
            CurrentLostTeams.Add(t);

            if (CurrentLostTeams.Count > LongesLostTeams.Length)
                LongesLostTeams = CurrentLostTeams.ToArray();
        }
    }
}
