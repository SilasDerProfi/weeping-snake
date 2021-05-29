using WeepingSnake.Game.Structs;

namespace WeepingSnake.WebService
{
    public static class GameBackend
    {
        private static Game.GameController _gameBackendController;

        public static Game.GameController Controller
        {
            get
            {
                if (_gameBackendController == null)
                {
                    var allowedPlaerCount = new PlayerRange(4, 10);
                    var boardDimensions = new BoardDimensions(600, 400);

                    _gameBackendController = new Game.GameController(allowedPlaerCount, boardDimensions);
                }

                return _gameBackendController;
            }
        }
    }
}
