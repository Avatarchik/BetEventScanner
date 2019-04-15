using System.Collections.Generic;

namespace ParimatchDayOddsParser
{
    public  class CyberFootballBetsState
    {
        public Dictionary<string, CyberFootballBet> Bets { get; set; } = new Dictionary<string, CyberFootballBet>();
    }
}