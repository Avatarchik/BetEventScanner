using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataAccess.DataModel;
using BetEventScanner.DataAccess.Entities;

namespace BetEventScanner.DataAccess
{
    public interface IDefaultUnitOfWork : IUnitOfWork
    {
        IRepository<FootballTeam> FootballTeams { get; }
        IRepository<BetInfo> BetInfoes { get; }
        IRepository<Line> Lines { get; }
    }
}
