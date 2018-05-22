using System;

namespace BetEventScanner.Common.Services.FootbalDataCoUk.Model
{
    public class Status
    {
        public bool Initialized { get; set; }

        public bool IsUpdated { get; set; }

        public DateTime LastCheck { get; set; }

        public DateTime CreationDateTime { get; set; }
    }
}
