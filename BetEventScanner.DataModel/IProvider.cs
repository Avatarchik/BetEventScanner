using System.Collections.Generic;

namespace BetEventScanner.DataModel
{
    public interface IProvider
    {
        IDictionary<string, string> Providers { get; set; }
    }
}