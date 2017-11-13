namespace BetEventScanner.SoccerstandScaner
{
    public interface IParserStorage<T>
    {
        T LoadOriginSource(string url);

        void Store(T data);
    }
}