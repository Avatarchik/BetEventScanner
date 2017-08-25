using System.Linq;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;
using BetEventScanner.DataAccess.DataModel;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.Common.Services
{
    public class TeamService
    {
        private IRepository<Team> _teamRepository;
        private IRepository<Country> _countryRepository;
        private readonly IDbProvider _sourceDbProvider;

        public TeamService()
        {
            _teamRepository = new Repository<Team>("Teams");
            _countryRepository = new Repository<Country>("Countries");
        }

        public void CopyTeams()
        {
            //_countryRepository.InsertEntity(new Country());

            var countries = _countryRepository.GetEntities();

            if (!countries.Any()) return;

            foreach (var country in countries)
            {
                
            }
        }   
    }
}
