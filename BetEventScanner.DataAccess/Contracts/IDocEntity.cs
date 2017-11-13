using MongoDB.Bson;

namespace BetEventScanner.DataAccess.Contracts
{
    public interface IDocEntity
    {
        ObjectId Id { get; set; }
    }
}