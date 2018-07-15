namespace BetEventScanner.Providers.Parimatch.Model
{
    public interface IParimatchEvent : IOddsBetEvent
    {
        string ParimatchId { get; set; }
    }
}