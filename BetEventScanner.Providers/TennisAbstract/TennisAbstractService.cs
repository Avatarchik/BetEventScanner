using System;
using BetEventScanner.Common.Services.TennisAbstract.DataSource;

namespace BetEventScanner.Providers.TennisAbstract
{
    public class TennisAbstractService
    {
        //private readonly BetEventScannerContext _context;

        //public TennisAbstractService()
        //{
        //    _context = new BetEventScannerContext();
        //}

        //public void StoreAtpPlayers()
        //{
        //    var source = new TennisAbstractParser().GetAtpPlayers(@"C:\BetEventScanner\tennis_atp-master\tennis_atp-master\atp_players.csv");
        //
        //    foreach (var tennisPlayer in source)
        //    {
        //        DateTime? birthDate = null;
        //        try
        //        {
        //            if (!string.IsNullOrEmpty(tennisPlayer.BirthDate))
        //            {
        //                birthDate = new DateTime(int.Parse(tennisPlayer.BirthDate.Substring(0, 4)), int.Parse(tennisPlayer.BirthDate.Substring(4, 2)), int.Parse(tennisPlayer.BirthDate.Substring(6, 2)));
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //        }
        //
        //        var entity = new TennisPlayer
        //        {
        //            OriginPlayerId = tennisPlayer.PlayerId,
        //            Sex = Sex.Male,
        //            FirstName = tennisPlayer.FirstName,
        //            LastName = tennisPlayer.LastName,
        //            Hand = tennisPlayer.Hand,
        //            BirthDate = birthDate,
        //            СountryCode = tennisPlayer.СountryCode
        //        };
        //
        //        _context.TennisPlayers.Add(entity);
        //
        //    }
        //
        //    _context.SaveChanges();
        //}
    }
}
