using System.Threading.Tasks;

namespace BetEventScanner.DataAccess
{
    public interface IUnitOfWork
    {
        int Commit();

        Task<int> CommitAsync();
    }
}
