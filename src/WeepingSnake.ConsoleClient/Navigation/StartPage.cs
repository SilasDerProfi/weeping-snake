using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.ConsoleClient.IO;
using WeepingSnake.Game;
using WeepingSnake.Game.Person;
using WeepingSnake.Game.Player;

namespace WeepingSnake.ConsoleClient.Navigation
{
    public class StartPage : UserInterface<GameController>
    {
        public StartPage(GameController data, IOHandler ioHandler) : base(data, ioHandler)
        {

        }

        private GameController GetGameController()
        {
            return Data;
        }

        internal override void OpenAndPrintPage()
        {
            InOut.Clear();
            InOut.WriteLine();
            InOut.WriteLine(@"__      __            _             ___           _       ");
            InOut.WriteLine(@"\ \    / /__ ___ _ __(_)_ _  __ _  / __|_ _  __ _| |_____ ");
            InOut.WriteLine(@" \ \/\/ / -_) -_) '_ \ | ' \/ _` | \__ \ ' \/ _` | / / -_)");
            InOut.WriteLine(@"  \_/\_/\___\___| .__/_|_||_\__, | |___/_||_\__,_|_\_\___|");
            InOut.WriteLine(@"                |_|         |___/                         ");
            InOut.WriteLine();
            InOut.WriteLine(@"==========================================================");
            InOut.WriteLine();

            InOut.WriteLine("Welcome to the offline Console-Client of the game weeping");
            InOut.WriteLine("snake!");
            InOut.WriteLine();

            InOut.WriteLine("You have the following options:");
            InOut.WriteLine(" - Quick Game (Q)");
            InOut.WriteLine(" - Login (L)");
            InOut.WriteLine(" - Register (R)");
            InOut.WriteLine(" - View Highscores (H)");

            var nextAction = ProcessInput();
            nextAction();
        }

        protected override Action ProcessInput()
        {
            var userInput = InOut.ReadLine().ToUpper();

            if (userInput == "Q")
            {
                return () =>
                {
                    var player = GetGameController().JoinGame();
                    new GamePage((GetGameController(), player), InOut).OpenAndPrintPage();
                };
            }
            else if (userInput == "L")
            {
                return () =>
                {
                    InOut.Write("Please enter your email address: ");
                    var mailAddress = InOut.ReadLine();

                    InOut.Write("Please enter your password: ");
                    var password = InOut.ReadLine();

                    var person = Person.Login(mailAddress, password);
                    
                    if(person == null)
                    {
                        PrintErrorAndNavigateTo(new StartPage(GetGameController(), InOut));
                    }
                    else
                    {
                        new UserPage((GetGameController(), person), InOut).OpenAndPrintPage();
                    }
                };
            }
            else if (userInput == "R")
            {
                return () =>
                {
                    InOut.Write("Please enter your email address: ");
                    var mailAddress = InOut.ReadLine();

                    InOut.Write("Please choose a username: ");
                    var username = InOut.ReadLine();

                    InOut.Write("Please choose a password: ");
                    var password = InOut.ReadLine();

                    InOut.Write("Please retype the password: ");
                    var passwordRetyped = InOut.ReadLine();

                    Person.Register(mailAddress, username, password, passwordRetyped);
                    var person = Person.Login(mailAddress, password);

                    if (person == null)
                    {
                        PrintErrorAndNavigateTo(new StartPage(GetGameController(), InOut));
                    }
                    else
                    {
                        new UserPage((GetGameController(), person), InOut).OpenAndPrintPage();
                    }
                };
            }
            else if (userInput == "H")
            {
                return () =>
                {
                    new ShowHighscoresPage((GetGameController(), null), InOut).OpenAndPrintPage();
                };
            }
            else
            {
                return () =>
                {
                    PrintErrorAndNavigateTo(new StartPage(GetGameController(), InOut));
                };
            }
        }
    }
}
