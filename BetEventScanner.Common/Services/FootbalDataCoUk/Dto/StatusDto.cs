using System;

namespace BetEventScanner.Common.Services.FootbalDataCoUk.Dto
{
    public class StatusDto
    {
        public bool Initialized { get; set; }

        public bool IsUpdated { get; set; }

        public DateTime LastCheck { get; set; }

        public DateTime CreationDateTime { get; set; }
    }
}
