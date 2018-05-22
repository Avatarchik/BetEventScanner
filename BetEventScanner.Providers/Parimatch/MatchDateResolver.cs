using System;
using System.Collections.Generic;
using System.Linq;

namespace BetEventScanner.Providers.Parimatch
{
    public class MatchDateResolver
    {
        private readonly DateTime _startDateTime;
        private readonly DateTime _endDateTime;
        private Dictionary<int, int> d = new Dictionary<int, int>();

        public MatchDateResolver(ICollection<string> dates)
        {
            var ordered = dates.OrderBy(x => x).ToList();
            var start = ordered.First();
            var end = ordered.Last();
            _startDateTime = DateTime.Parse($"{start.Substring(0, 4)}-{start.Substring(4, 2)}-{start.Substring(6, 2)}");
            _endDateTime = DateTime.Parse($"{end.Substring(0, 4)}-{end.Substring(4, 2)}-{end.Substring(6, 2)}");
            var t = _startDateTime;
            while (t <= _endDateTime)
            {
                d.Add(t.Month, t.Year);
                t = t.AddMonths(1);
            }
        }

        public MatchDateResolver(string url) : this(new List<string> { url })
        {
        }

        public DateTime GetDate(string shortDate, string time)
        {
            var md = shortDate.Split('/');
            var month = int.Parse(md[1]);
            var day = int.Parse(md[0]);
            var t = time.Split(':');
            var hours = int.Parse(t[0]);
            var minutes = int.Parse(t[1]);

            if (d.ContainsKey(month))
            {
                return new DateTime(d[month], month, day, hours, minutes, 0);
            }

            var addDate = _endDateTime.AddMonths(month - _endDateTime.Month);

            d.Add(addDate.Month, addDate.Year);

            return new DateTime(d[month], month, day, hours, minutes, 0);
        }
    }
}
