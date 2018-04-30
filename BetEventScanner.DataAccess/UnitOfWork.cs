using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataAccess.DataModel;
using BetEventScanner.DataAccess.EF;
using System;
using System.Threading.Tasks;

namespace BetEventScanner.DataAccess
{
    public class UnitOfWork : IDefaultUnitOfWork, IDisposable
    {
        protected readonly BetEventScannerContext Context = new BetEventScannerContext();
        private bool _disposed;

        private IRepository<FootballTeam> _footballTeams;
        private IRepository<Country> _counties;
        private IRepository<City> _cities;

        public IRepository<FootballTeam> FootballTeams => _footballTeams ?? (_footballTeams = new Repository<FootballTeam>(Context));

        public IRepository<Country> Counties => _counties ?? (_counties = new Repository<Country>(Context));

        public IRepository<City> Cities => _cities ?? (_cities = new Repository<City>(Context));

        public int Commit()
        {
            return Context.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public interface IDefaultUnitOfWork : IUnitOfWork
    {
        IRepository<FootballTeam> FootballTeams { get; }
    }

    public interface IUnitOfWork
    {
        int Commit();

        Task<int> CommitAsync();
    }
}
