using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using BetEventScanner.DataAccess.DataModel;
using BetEventScanner.DataAccess.EF;

namespace BetEventScanner.Common.Services
{
    public class FlatBetStorage
    {
        private readonly BetEventScannerContext _context;

        public FlatBetStorage()
        {
            _context = new BetEventScannerContext();
        }

        public async Task<ICollection<FlatBet>> GetAllAsync()
        {
            return await _context.FlatTennisBets.ToListAsync();
        }

        public async Task Add(FlatBet flatBet)
        {
            _context.FlatTennisBets.Add(flatBet);
            await _context.SaveChangesAsync();
        }
    }

    public class BubbelLadderFromThree
    {
        private FlatBetStorage _flatBetStorage;

        public BubbelLadderFromThree()
        {
            _flatBetStorage = new FlatBetStorage();
        }

        public async void GetCalculation()
        {
            var flatBets = await _flatBetStorage.GetAllAsync();
        }

        public async void AddFlatBet(FlatBet flatBet)
        {
            await _flatBetStorage.Add(flatBet);
        }
    }
}
