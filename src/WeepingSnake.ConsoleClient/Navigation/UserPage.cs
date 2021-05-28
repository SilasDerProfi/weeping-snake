using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeepingSnake.ConsoleClient.IO;
using WeepingSnake.Game;
using WeepingSnake.Game.Person;

namespace WeepingSnake.ConsoleClient.Navigation
{
    public class UserPage : UserInterface<(GameController, Person)>
    {
        public UserPage((GameController, Person) data, IOHandler ioHandler) : base(data, ioHandler)
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
            InOut.Clear();
            InOut.WriteLine($"Welcome {GetPerson().Username}!");
            InOut.WriteLine();

            InOut.WriteLine("You have the following options:");
            InOut.WriteLine(" - Join a Game (J)");
            InOut.WriteLine(" - Change your password (P)");
            InOut.WriteLine(" - Change your email address (M)");
            InOut.WriteLine(" - View Highscores (H)");
            InOut.WriteLine(" - Log out (L)");

            var nextAction = ProcessInput();
            nextAction();
        }

        protected override Action ProcessInput()
        {
            var userInput = InOut.ReadLine().ToUpper();

            if (userInput == "J")
            {
                return () =>
                {
                    var player = GetGameController().JoinGame(GetPerson());
                    new GamePage((GetGameController(), player), InOut).OpenAndPrintPage();
                };
            }
            else if (userInput == "P")
            {
                return () =>
                {
                    InOut.Write("Please choose a password: ");
                    var password = InOut.ReadLine();

                    InOut.Write("Please retype the password: ");
                    var passwordRetyped = InOut.ReadLine();

                    var changeSuccessful = GetPerson().ChangePassword(password, passwordRetyped);

                    if(changeSuccessful)
                    {
                        PrintChangeSuccessfulAndNavigateBack();
                    }
                    else
                    {
                        PrintErrorAndNavigateTo(new UserPage((GetGameController(), GetPerson()), InOut));
                    }
                };
            }
            else if (userInput == "M")
            {
                return () =>
                {
                    InOut.Write("Please enter your email address: ");
                    var mailAddress = InOut.ReadLine();

                    var changeSuccessful = GetPerson().ChangeEmail(mailAddress);

                    if (changeSuccessful)
                    {
                        PrintChangeSuccessfulAndNavigateBack();
                    }
                    else
                    {
                        PrintErrorAndNavigateTo(new UserPage((GetGameController(), GetPerson()), InOut));
                    }
                };
            }
            else if (userInput == "H")
            {
                return () =>
                {
                    new ShowHighscoresPage((GetGameController(), GetPerson()), InOut).OpenAndPrintPage();
                };
            }
            else if (userInput == "L")
            {
                return () =>
                {
                    new StartPage(GetGameController(), InOut).OpenAndPrintPage();
                };
            }
            else
            {
                return () =>
                {
                    PrintErrorAndNavigateTo(new UserPage((GetGameController(), GetPerson()), InOut));
                };
            }
        }

        private void PrintChangeSuccessfulAndNavigateBack()
        {
            InOut.WriteLine("Changed successful. Press any key.");
            InOut.ReadKey();
            new UserPage((GetGameController(), GetPerson()), InOut).OpenAndPrintPage();
        }
    }
}
