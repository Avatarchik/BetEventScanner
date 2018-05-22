using System.Collections.Generic;
using BetEventScanner.Common.Services.Csv;
using BetEventScanner.Common.Services.TennisAbstract.DataSource;
using BetEventScanner.Common.Services.TennisAbstract.Model;

namespace BetEventScanner.Providers.TennisAbstract.DataSource
{
    internal class TennisAbstractParser : CsvParserBase
    {
        public ICollection<AtpPlayer> GetAtpPlayers(string filePath)
        {
            //return Parse<AtpPlayer>(filePath, new TennisAbstractPlayerMapping());
            return null;
        }
    }
}
