using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game;
using WeepingSnake.Game.Person;

namespace WeepingSnake.ConsoleClient.Navigation
{
    public class UserPage : IUserInterface<(GameController, Person)>
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
            Console.WriteLine($"Welcome {_person.Username}!");
            Console.WriteLine();

            Console.WriteLine("You have the following options:");
            Console.WriteLine(" - Join a Game (J)");
            Console.WriteLine(" - Change your password (P)");
            Console.WriteLine(" - Change your email address (M)");
            Console.WriteLine(" - View Highscores (H)");
            Console.WriteLine(" - Log out (L)");

            var nextAction = ProcessInput();
            nextAction();
        }

        public Action ProcessInput()
        {
            var userInput = Console.ReadLine().ToUpper();

            if (userInput == "J")
            {
                return () =>
                {
                    var player = _gameController.JoinGame(_person);
                    new GamePage().Open(player);
                };
            }
            else if (userInput == "P")
            {
                return () =>
                {
                    Console.Write("Please choose a password: ");
                    var password = Console.ReadLine();

                    Console.Write("Please retype the password: ");
                    var passwordRetyped = Console.ReadLine();

                    var changeSuccessful = _person.ChangePassword(password, passwordRetyped);

                    if(changeSuccessful)
                    {
                        Console.WriteLine("Changed successful. Press any key.");
                        Console.ReadKey();
                        new UserPage().Open((_gameController, _person));
                    }
                    else
                    {
                        Console.WriteLine("There was an error. Press any key.");
                        Console.ReadKey();
                        new UserPage().Open((_gameController, _person));
                    }
                };
            }
            else if (userInput == "M")
            {
                return () =>
                {
                    Console.Write("Please enter your email address: ");
                    var mailAddress = Console.ReadLine();

                    var changeSuccessful = _person.ChangeEmail(mailAddress);

                    if (changeSuccessful)
                    {
                        Console.WriteLine("Changed successful. Press any key.");
                        Console.ReadKey();
                        new UserPage().Open((_gameController, _person));
                    }
                    else
                    {
                        Console.WriteLine("There was an error. Press any key.");
                        Console.ReadKey();
                        new UserPage().Open((_gameController, _person));
                    }
                };
            }
            else if (userInput == "H")
            {
                return () =>
                {
                    new ShowHighscoresPage().Open((_gameController, _person));
                };
            }
            else if (userInput == "L")
            {
                return () =>
                {
                    new StartPage().Open(_gameController);
                };
            }
            else
            {
                return () =>
                {
                    Console.WriteLine("There was an error. Press any key.");
                    Console.ReadKey();
                    new UserPage().Open((_gameController, _person));
                };
            }
        }
    }
}
