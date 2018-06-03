using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetEventScanner.DogonWeb.Helpers
{
    public static class Extensions
    {
        public static IEnumerable<TSource> TakeUntil<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return _(); IEnumerable<TSource> _()
            {
                foreach (var item in source)
                {
                    yield return item;
                    if (predicate(item))
                        yield break;
                }
            }
        }
    }
}