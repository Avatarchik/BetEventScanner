namespace BetEventScanner.DataAccess.Providers
{
    public interface IDbProvider
    {
        void StoreData(object data);
    }
}