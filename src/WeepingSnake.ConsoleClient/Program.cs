using WeepingSnake.ConsoleClient.Navigation;
using WeepingSnake.Game.Structs;

namespace WeepingSnake.ConsoleClient
{
    static class Program
    {
        static void Main(string[] args)
        {
            var playerRange = new PlayerRange(2, 5);
            var boardDimensions = new BoardDimensions(10, 20);
            var gameController = new Game.GameController(playerRange, boardDimensions);

            var startPage = new StartPage(gameController);
            startPage.OpenAndPrintPage();

            gameController.Dispose();
        }
    }
}
