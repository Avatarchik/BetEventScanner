using System.Collections.Generic;
using System.Linq;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;

namespace BetEventScanner.Common.Services.FootballDataOrg
{
    public class FootballDataOrgCountryMap : ICountryMap
    {
        private readonly IDictionary<CountryDivision, int> _idIdMap = new Dictionary<CountryDivision, int>
        {
            { CountryDivision.England1, 398 },
            { CountryDivision.England2, 427 },
            { CountryDivision.Germany1, 394 },
            { CountryDivision.Germany2, 395 },
            { CountryDivision.Italy1, 438 },
            { CountryDivision.Spain1, 436 },
            { CountryDivision.Spain2, 437 }
        };

        private readonly IDictionary<CountryDivision, CountryCode> _codeMap = new Dictionary<CountryDivision, CountryCode>
        {
            { CountryDivision.England1, CountryCode.PL },
            { CountryDivision.England2, CountryCode.ELC },
            { CountryDivision.Germany1, CountryCode.BL1 },
            { CountryDivision.Germany2, CountryCode.BL2 },
            { CountryDivision.Italy1,   CountryCode.SA },
            { CountryDivision.Italy2,   CountryCode.SB },
            { CountryDivision.Spain1,   CountryCode.PD },
            { CountryDivision.Spain2,   CountryCode.SD }
        };

        public IDictionary<CountryDivision, int> IdMap => _idIdMap;

        public IDictionary<CountryDivision, CountryCode> CodeMap => _codeMap;

        public CountryDivision GetCompetitionById(int divisionId)
        {
            return _idIdMap.FirstOrDefault(x => x.Value.Equals(divisionId)).Key;
        }

        public CountryDivision GetCompetitionByCode(string code)
        {
            return _codeMap.FirstOrDefault(x => x.Value.ToString().Equals(code)).Key;
        }
    }
}