using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.Game;
using WeepingSnake.Game.Person;

namespace WeepingSnake.ConsoleClient.Navigation
{
    public class UserPage : UserInterface<(GameController, Person)>
    {
        public UserPage((GameController, Person) data) : base(data)
        {

        }

        private GameController GetGameController()
        {
            return Data.Item1;
        }

        private Person GetPerson()
        {
            return Data.Item2;
        }

        internal override void OpenAndPrintPage()
        {
            Console.Clear();
            Console.WriteLine($"Welcome {GetPerson().Username}!");
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

        protected override Action ProcessInput()
        {
            var userInput = Console.ReadLine().ToUpper();

            if (userInput == "J")
            {
                return () =>
                {
                    var player = GetGameController().JoinGame(GetPerson());
                    new GamePage((GetGameController(), player)).OpenAndPrintPage();
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

                    var changeSuccessful = GetPerson().ChangePassword(password, passwordRetyped);

                    if(changeSuccessful)
                    {
                        PrintChangeSuccessfulAndNavigateBack();
                    }
                    else
                    {
                        PrintErrorAndNavigateTo(new UserPage((GetGameController(), GetPerson())));
                    }
                };
            }
            else if (userInput == "M")
            {
                return () =>
                {
                    Console.Write("Please enter your email address: ");
                    var mailAddress = Console.ReadLine();

                    var changeSuccessful = GetPerson().ChangeEmail(mailAddress);

                    if (changeSuccessful)
                    {
                        PrintChangeSuccessfulAndNavigateBack();
                    }
                    else
                    {
                        PrintErrorAndNavigateTo(new UserPage((GetGameController(), GetPerson())));
                    }
                };
            }
            else if (userInput == "H")
            {
                return () =>
                {
                    new ShowHighscoresPage((GetGameController(), GetPerson())).OpenAndPrintPage();
                };
            }
            else if (userInput == "L")
            {
                return () =>
                {
                    new StartPage(GetGameController()).OpenAndPrintPage();
                };
            }
            else
            {
                return () =>
                {
                    PrintErrorAndNavigateTo(new UserPage((GetGameController(), GetPerson())));
                };
            }
        }

        private void PrintChangeSuccessfulAndNavigateBack()
        {
            Console.WriteLine("Changed successful. Press any key.");
            Console.ReadKey();
            new UserPage((GetGameController(), GetPerson())).OpenAndPrintPage();
        }
    }
}
