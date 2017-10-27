using System.Collections.Generic;
using System.Linq;
using BetEventScanner.Common.Contracts;
using BetEventScanner.Common.Services;
using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataAccess.DataModel;
using BetEventScanner.DataAccess.DataModel.DbEntities;
using BetEventScanner.DataAccess.Mongo;

namespace BetEventScanner.ConsoleApp
{
    public class BettongService
    {
        private IRepository<BettingEntity> _repository = new MongoRepository<BettingEntity>("Bets");

        public BettingEntity BettingEntity { get; private set; }

        public void AddMatch(Tmatch match)
        {
            BettingEntity.Matches.Add(match);
        }

        public void AddBet(Tbet bet)
        {
            var match = BettingEntity.Matches.FirstOrDefault(x => x.MatchId == bet.MatchId);
            match?.AddBet(bet);
        }

        public void AddOppositeBet(ToppositBet oppositBet)
        {
            BettingEntity.OppositBets.Add(oppositBet);
        }

        public void AddResult(Tresult result)
        {
            BettingEntity.Results.Add(result);
        }

        public void Load()
        {
            var bettingEntity = _repository.GetEntities().FirstOrDefault();

            if (bettingEntity == null)
            {
                bettingEntity = new BettingEntity();
                _repository.InsertEntity(bettingEntity);
                BettingEntity = bettingEntity;
                return;
            }

            BettingEntity = bettingEntity;
        }

        public void Save()
        {
            _repository.Update(BettingEntity);
        }

        public int GetId()
        {
            return ++BettingEntity.InternalId;
        }

        public IEnumerable<Tmatch> GetMatches()
        {
            return BettingEntity.Matches;
        }
    }
}