namespace BetEventScanner.Providers.Parimatch
{
    public interface IOddsBetEvent
    {
        EventType EventType { get; set; }

        string Header { get; set; }
    }
}