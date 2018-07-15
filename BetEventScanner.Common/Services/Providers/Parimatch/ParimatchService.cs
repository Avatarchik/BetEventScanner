using BetEventScanner.DataAccess;

namespace BetEventScanner.Providers.Parimatch
{
    public class ParimatchService
    {
        private readonly IDefaultUnitOfWork unitOfWork;

        public ParimatchService(IDefaultUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
