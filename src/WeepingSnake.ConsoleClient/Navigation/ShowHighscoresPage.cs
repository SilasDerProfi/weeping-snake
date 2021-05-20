using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game;
using WeepingSnake.Game.Person;

namespace WeepingSnake.ConsoleClient.Navigation
{
    public class ShowHighscoresPage : IUserInterface<(GameController, Person)>
    {
        private GameController _gameController;
        private Person _person;

        public void Open((GameController, Person) data)
        {
            _gameController = data.Item1;
            _person = data.Item2;

            PrintPage();
        }

        public void PrintPage()
        {
            Console.Clear();
            Console.WriteLine(@" _  _ _      _                          ");
            Console.WriteLine(@"| || (_)__ _| |_  ___ __ ___ _ _ ___ ___");
            Console.WriteLine(@"| __ | / _` | ' \(_-</ _/ _ \ '_/ -_|_-<");
            Console.WriteLine(@"|_||_|_\__, |_||_/__/\__\___/_| \___/__/");
            Console.WriteLine(@"       |___/                            ");
            Console.WriteLine();
            Console.WriteLine(@"========================================");
            Console.WriteLine();

            PrintHighscores();

            Console.WriteLine();
            Console.WriteLine("You have the following options:");
            Console.WriteLine(" - Go Back (B)");

            var nextAction = ProcessInput();
            nextAction();
        }

        public Action ProcessInput()
        {
            var userInput = Console.ReadLine().ToUpper();

            if (userInput == "B")
            {
                return () =>
                {
                    if(_person == null)
                    {
                        new StartPage().Open(_gameController);
                    }
                    else
                    {
                        new UserPage().Open((_gameController, _person));
                    }
                };
            }
            else
            {
                return () =>
                {
                    Console.WriteLine("There was an error. Press any key.");
                    Console.ReadKey();
                    new ShowHighscoresPage().Open((_gameController, _person));
                };
            }
        }

        private void PrintHighscores()
        {
            Console.WriteLine($" Username    | Max. Points | Total Points | Played Games");
            Console.WriteLine("---------------------------------------------------------");

            foreach (var highscoreEntry in HighscoreEntry.GetHighscoreEntries())
            {
                Console.WriteLine($" {highscoreEntry.Username:12}| {highscoreEntry.MaximumPointsInGame:12}| {highscoreEntry.TotalPoints: 13}| {highscoreEntry.PlayedGames:11}");
            }
        }
    }
}
