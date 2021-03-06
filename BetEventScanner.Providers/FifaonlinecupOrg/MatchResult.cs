﻿using BetEventScanner.DataModel;
using System;

namespace BetEventScanner.Providers.FifaonlinecupOrg
{
    public class MatchResult
    {
        public DateTime Date { get; set; }

        public string Tournament { get; set; }

        public CyberFootballPlayer Player1 { get; set; }

        public CyberFootballPlayer Player2 { get; set; }

        public string HT { get; set; }

        public string FT { get; set; }

        public string Status { get; set; } = "ok";
    }
}
