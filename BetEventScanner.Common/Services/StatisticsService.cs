using BetEventScanner.Common.DataModel;
using BetEventScanner.DataAccess.DataModel.DbEntities;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.Common.Services
{
    public class StatisticsService
    {
        private readonly GlobalSettings _settings;
        private readonly MongoDbProvider _mongo;

        public StatisticsService(GlobalSettings settings, MongoDbProvider mongo)
        {
            _settings = settings;
            _mongo = mongo;
        }

        //public void UpdateTeamTotalCoeffAmaunt()
        //{
        //    var countries = _settings.SupportedCountries;
        //    foreach (var country in countries)
        //    {
        //        _mongo.GetEntity<CountryCompetitionsStatisticsEntity>(country.ToString(), 5);
        //    }
        //}
    }
}