using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game;
using WeepingSnake.Game.Person;
using WeepingSnake.Game.Player;

namespace WeepingSnake.ConsoleClient.Navigation
{
    public class StartPage : UserInterface<GameController>
    {
        public StartPage(GameController data) : base(data)
        {

        }

        private GameController GetGameController()
        {
            return Data;
        }

        internal override void OpenAndPrintPage()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(@"__      __            _             ___           _       ");
            Console.WriteLine(@"\ \    / /__ ___ _ __(_)_ _  __ _  / __|_ _  __ _| |_____ ");
            Console.WriteLine(@" \ \/\/ / -_) -_) '_ \ | ' \/ _` | \__ \ ' \/ _` | / / -_)");
            Console.WriteLine(@"  \_/\_/\___\___| .__/_|_||_\__, | |___/_||_\__,_|_\_\___|");
            Console.WriteLine(@"                |_|         |___/                         ");
            Console.WriteLine();
            Console.WriteLine(@"==========================================================");
            Console.WriteLine();

            Console.WriteLine("Welcome to the offline Console-Client of the game weeping");
            Console.WriteLine("snake!");
            Console.WriteLine();

            Console.WriteLine("You have the following options:");
            Console.WriteLine(" - Quick Game (Q)");
            Console.WriteLine(" - Login (L)");
            Console.WriteLine(" - Register (R)");
            Console.WriteLine(" - View Highscores (H)");

            var nextAction = ProcessInput();
            nextAction();
        }

        protected override Action ProcessInput()
        {
            var userInput = Console.ReadLine().ToUpper();

            if (userInput == "Q")
            {
                return () =>
                {
                    var player = GetGameController().JoinGame();
                    new GamePage((GetGameController(), player)).OpenAndPrintPage();
                };
            }
            else if (userInput == "L")
            {
                return () =>
                {
                    Console.Write("Please enter your email address: ");
                    var mailAddress = Console.ReadLine();

                    Console.Write("Please enter your password: ");
                    var password = Console.ReadLine();

                    var person = Person.Login(mailAddress, password);
                    
                    if(person == null)
                    {
                        PrintErrorAndNavigateTo(new StartPage(GetGameController()));
                    }
                    else
                    {
                        new UserPage((GetGameController(), person)).OpenAndPrintPage();
                    }
                };
            }
            else if (userInput == "R")
            {
                return () =>
                {
                    Console.Write("Please enter your email address: ");
                    var mailAddress = Console.ReadLine();

                    Console.Write("Please choose a username: ");
                    var username = Console.ReadLine();

                    Console.Write("Please choose a password: ");
                    var password = Console.ReadLine();

                    Console.Write("Please retype the password: ");
                    var passwordRetyped = Console.ReadLine();

                    Person.Register(mailAddress, username, password, passwordRetyped);
                    var person = Person.Login(mailAddress, password);

                    if (person == null)
                    {
                        PrintErrorAndNavigateTo(new StartPage(GetGameController()));
                    }
                    else
                    {
                        new UserPage((GetGameController(), person)).OpenAndPrintPage();
                    }
                };
            }
            else if (userInput == "H")
            {
                return () =>
                {
                    new ShowHighscoresPage((GetGameController(), null)).OpenAndPrintPage();
                };
            }
            else
            {
                return () =>
                {
                    PrintErrorAndNavigateTo(new StartPage(GetGameController()));
                };
            }
        }
    }
}
