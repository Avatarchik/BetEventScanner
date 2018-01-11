using System.Collections.Generic;
using BetEventScanner.Common.Services.TennisAbstract.Model;

namespace BetEventScanner.Common.Services.TennisAbstract.DataSource
{
    internal class TennisAbstractParser : CsvParserBase
    {
        public ICollection<AtpPlayer> GetAtpPlayers(string filePath)
        {
            return Parse<AtpPlayer>(filePath, new TennisAbstractPlayerMapping());
        }
    }
}
