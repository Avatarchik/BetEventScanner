using System.Collections.Generic;

namespace BetEventScanner.DataModel.Contracts
{
    public interface IProvider
    {
        IDictionary<string, string> Providers { get; set; }
    }
}