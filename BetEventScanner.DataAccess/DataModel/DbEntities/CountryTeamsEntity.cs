﻿using System.Collections.Generic;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.DataAccess.DataModel.DbEntities
{
    public class CountryTeamsEntity : IDocEntity
    {
        public int Id { get; set; } = 2;

        public bool Uploaded { get; set; } = true;

        public List<TeamEntity> Teams { get; set; } = new List<TeamEntity>();
    }
}
