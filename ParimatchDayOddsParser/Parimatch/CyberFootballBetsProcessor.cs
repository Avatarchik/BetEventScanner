using BetEventScanner.Providers.Parimatch.Models.CyberFootball;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace ParimatchDayOddsParser
{
    public class CyberFootballBetsProcessor
    {
        private CyberFootballBetsState _state;
        private static DirectoryInfo _dir = new DirectoryInfo(@"C:\BetEventScanner\cyberFootball\bets");
        private static DirectoryInfo _dirSn = new DirectoryInfo(@"C:\BetEventScanner\cyberFootball\snapshots");

        public CyberFootballBetsProcessor()
        {
            _state = new CyberFootballBetsState();

            var files = _dir.GetFiles();
            if (files.Any())
            {
                _state.Bets = files
                .Select(x => LoadBet(x.FullName))
                .ToDictionary(k => k.EvNo, v => v);
            }
        }

        public void AddBet(CyberFootballBet bet) => StoreBet(bet);

        public void AddSnapshot(CyberFootballMatch match) => StoreSnapshot(match);

        private void StoreSnapshot(CyberFootballMatch match)
        {
            var name = match.EventNo + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".json";
            File.WriteAllText(_dirSn.FullName + "\\" + name, JsonConvert.SerializeObject(match));
        }

        public bool BetExists(string evno) =>
            _state.Bets.ContainsKey(evno);

        private static CyberFootballBet LoadBet(string path) =>
            JsonConvert.DeserializeObject<CyberFootballBet>(File.ReadAllText(path));

        private static void StoreBet(CyberFootballBet bet)
        {
            var name = bet.EvNo +"_"+ DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".json";
            File.WriteAllText(_dir.FullName + "\\" + name, JsonConvert.SerializeObject(bet));
        }
    }
}
