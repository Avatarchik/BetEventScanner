﻿using System.Collections.Generic;
using BetEventScanner.DataAccess.Entities;

namespace BetEventScanner.DataAccess.DataModel
{
    public class FootballSeason
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public string Division { get; set; }

        public string DivisionCode { get; set; }

        public int StartYear { get; set; }

        public int EndYear { get; set; }

        public bool IsCurrent { get; set; }

        public List<FootballMatchResult> Results { get; set; }
    }
}