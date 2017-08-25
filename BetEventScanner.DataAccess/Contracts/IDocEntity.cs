using MongoDB.Bson;

namespace BetEventScanner.DataAccess.Contracts
{
    public interface IDocEntity
    {
        ObjectId Id { get; set; }
    }

    public interface ISqlEntity
    {
        int SqlId { get; set; }
    }
}