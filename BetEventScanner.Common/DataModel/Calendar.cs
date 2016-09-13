using System;

namespace BetEventScanner.Common.DataModel
{
    public class Calendar
    {
        public int Id { get; set; }
        public bool Uploaded { get; set; }
        public DateTime UploadDataTime { get; set; }
    }
}
