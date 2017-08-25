using System;
using System.Web.Http;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.DataModel;
using BetEventScanner.Common.Services;

namespace BetEventScanner.Web.Client.Controllers
{
    public class TestController : ApiController
    {
        private readonly IRepository<LeagueOdds> _oddsRepository;

        public TestController()
        {
            _oddsRepository = new OddsRepository<LeagueOdds>();
        }

        [HttpGet]
        public void Add()
        {
            _oddsRepository.InsertEntity(new LeagueOdds
            {
                Division = Division.EnglandApl,
                Source = OddsSource.BetClickCom,
                OriginSourceLeagueId = 666,
                Name = "Test",
                Created = DateTime.UtcNow
            });
        }
    }
}
