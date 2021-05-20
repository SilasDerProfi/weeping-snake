using WeepingSnake.ConsoleClient.Navigation;
using WeepingSnake.Game.Structs;

namespace WeepingSnake.ConsoleClient
{
    static class Program
    {
        static void Main(string[] args)
        {
            var playerRange = new PlayerRange(4, 6);
            var boardDimensions = new BoardDimensions(20, 20);
            var gameController = new Game.GameController(playerRange, boardDimensions);

            var startPage = new StartPage();
            startPage.Open(gameController);
        }
    }
}
