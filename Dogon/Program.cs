using System;
using System.IO;
using Dogon.Model;
using Newtonsoft.Json;

namespace Dogon
{
    class Program
    {
        static void Main(string[] args)
        {

            Func<BankStorage> bankSourceStrategy = () => JsonConvert.DeserializeObject<BankStorage>(File.ReadAllText(@"c:\\Bets\dogonBank.json"));
            Action<BankStorage> bankStoreStrategy = x => File.WriteAllText(@"c:\\Bets\dogonBank.json", JsonConvert.SerializeObject(x));

            // Init satorage
            //File.WriteAllText(@"c:\\Bets\dogonBank.json", JsonConvert.SerializeObject(new State()));

            var state = new StateStorage(bankSourceStrategy, bankStoreStrategy);
            var game = new Game(state);

            while (true)
            {
                try
                {
                    ShowMenu();
                    var choice = Console.ReadLine();
                    if (choice == "q")
                    {
                        break;
                    }

                    switch (choice)
                    {
                        case "1":
                            AddBets(game);
                            break;

                        case "2":
                            AddResults(game);
                            break;
                    }

                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        private static void ShowMenu()
        {
            Console.WriteLine("1. Add bets");
            Console.WriteLine("2. Add results");
        }

        private static void AddBets(Game game)
        {
            var favorite = new Bet();
            Console.WriteLine("Favorite odds");
            var favoriteOdds = Console.ReadLine();
            favorite.Odds = decimal.Parse(favoriteOdds);

            var underdog = new Bet();
            Console.WriteLine("Underdog odds");
            var underdogOdds = Console.ReadLine();
            underdog.Odds = decimal.Parse(underdogOdds);

            game.MakeBet(new ReverseBet
            {
                Favorite = favorite,
                Underdog = underdog
            });
        }

        private static void AddResults(Game game)
        {
            var bet = game.GetBetWithoutResult();

            Console.WriteLine("Favorite bet");
            Console.WriteLine("Favorite odds" + bet.Favorite.Odds);
            Console.WriteLine("Favorite sum" + bet.Underdog.BetSum);
            var bet1IsWon = Console.ReadLine() == "1";

            Console.WriteLine("Underdog bet");
            Console.WriteLine("Underdog odds" + bet.Favorite.Odds);
            Console.WriteLine("Underdog sum" + bet.Underdog.BetSum);
            var bet2IsWon = Console.ReadLine() == "1";

            if (bet1IsWon == bet2IsWon)
            {
                throw new Exception("One bet must be losed");
            }

            bet.Favorite.IsWon = bet1IsWon;
            bet.Underdog.IsWon = bet2IsWon;
            bet.Favorite.BetStatus = BetStatus.Finished;
            bet.Underdog.BetStatus = BetStatus.Finished;

            game.UpdateBetWithResult(bet);
        }
    }
}
