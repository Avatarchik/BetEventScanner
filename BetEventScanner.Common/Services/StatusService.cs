using System;
using System.Linq;
using BetEventScanner.Common.DataModel;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.Common.Services
{
    public class StatusService : IStatusService
    {
        private string _collectionName = "Status";
        private readonly IDbProvider _dbProvider = new MongoDbProvider("footballdb");

        public Status GetCurrentStatus()
        {
            var statuses = _dbProvider.GetEntities<Status>(_collectionName);

            if (statuses.Any())
            {
                return statuses.SingleOrDefault(x => x.Created.Date == DateTime.Today);
            }

            return null;
        }

        public void StoreStatus(Status status)
        {
            _dbProvider.InsertEntity(_collectionName, status);
        }
    }
}
