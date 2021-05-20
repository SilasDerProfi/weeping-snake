using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WeepingSnake.Game.Player;
using WeepingSnake.Game.Structs;

namespace WeepingSnake.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly WeepingSnake.Game.GameController _gameBackendController;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
            
            var allowedPlaerCount = new PlayerRange(4, 10);
            var boardDimensions = new BoardDimensions(600, 400);

            _gameBackendController = new Game.GameController(allowedPlaerCount, boardDimensions);
        }

        /// <summary>
        /// Register for a new game as guest. The result is a unique playerId (only valid for exactly this game).
        /// </summary>
        /// <returns>A unique player id for exactly one game.</returns>
        [HttpPut]
        public Guid JoinGame()
        {
            var joinedPlayer = _gameBackendController.JoinGame();
            return joinedPlayer.PlayerId;
        }

        /// <summary>
        /// Register for a new game as registered person. The result is a unique playerId (only valid for exactly this game).
        /// </summary>
        /// <param name="personId">The personId of the one who wants to participate in a game.</param>
        /// <returns>A unique player id for exactly one game.</returns>
        [HttpPut("{id}")]
        public Guid JoinGame(Guid personId)
        {
            var person = WeepingSnake.Game.Person.Person.GetById(personId);

            if (person != null)
            {
                var joinedPlayer = _gameBackendController.JoinGame(person);
                return joinedPlayer.PlayerId;
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Requests the game state for a game in which a specific player is participating.
        /// </summary>
        /// <param name="playerId">The id of the player for which the game state is requested.</param>
        /// <returns>The state of the game, if the specified player exists and is involved in a game.</returns>
        [HttpGet("{id}")]
        public object GetGameState(Guid playerId)
        {
            var player = _gameBackendController.FindPlayer(playerId);

            if (player == null)
                return null;

            var playerData = new 
            { 
                Points = player.Points, 
                IsAlive = player.IsAlive 
            };

            var allPaths = player.AssignedGame.BoardPaths;

            var currentRoundNumber = allPaths.Count;
            var playerLinesStartingRoundIndex = Math.Max(0, currentRoundNumber - 6);
            var roundsWithPlayerLines = currentRoundNumber - playerLinesStartingRoundIndex;

            var validPaths = allPaths.GetRange(playerLinesStartingRoundIndex, roundsWithPlayerLines).SelectMany(paths => paths).ToList());

            var validGameDistances = player.AssignedGame.BoardPaths.SelectMany(paths => paths);

            var boardData = validGameDistances.Select(d => new
            {
                Player = d.Player.PlayerId.GetHashCode(),
                StartX = d.StartX,
                StartY = d.StartY,
                EndX = d.EndX,
                EndY = d.EndY
            });


            var gameState = new 
            {
                Player = playerData,
                Board = boardData
            };

            return gameState;
        }

        /// <summary>
        /// Adds an action to be performed for a player.
        /// </summary>
        /// <param name="playerId">The id of the player that should perform this action.</param>
        /// <param name="action">The action-Number to be performed.</param>
        [HttpPost]
        public void SendAction(Guid playerId, int actionNumber)
        {
            var player = _gameBackendController.FindPlayer(playerId);
            
            if(player == null)
            {
                return;
            }

            var action = Enum.GetValues<PlayerAction.Action>()[actionNumber];

            _gameBackendController.DoAction(player, action);
        }

        /// <summary>
        /// Request the current time of the server.
        /// </summary>
        /// <returns>The time of the server when the request was processed (DateTimeOffSet).</returns>
        [HttpGet]
        public DateTimeOffset GetServerTime()
        {
            var currentTime = DateTimeOffset.Now;

            return currentTime;
        }

        /// <summary>
        /// Registers a new Person
        /// </summary>
        /// <param name="email">Email as reference for the person (have to be unique)</param>
        /// <param name="username">wished username (does not have to be unique)</param>
        /// <param name="password">chosen password</param>
        /// <param name="retypePassword">retyped password to avoid typos</param>
        /// <returns>The personId of the registered person or <see cref="Guid.Empty"/> if registration failed</returns>
        [HttpPut]
        public Guid Register(string email, string username, string password, string retypePassword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Login a already registered Person
        /// </summary>
        /// <param name="email">The email linked to the person</param>
        /// <param name="password">The chosen password</param>
        /// <returns>The personId of the registered person or <see cref="Guid.Empty"/> if login failed</returns>
        [HttpGet]
        public Guid Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the complete Highscore-List
        /// </summary>
        /// <returns>List of Highscore entries</returns>
        [HttpGet]
        public List<object> GetHighscores()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an email from a Person
        /// </summary>
        /// <param name="personId">The ID of the person whose email is to be changed.</param>
        /// <param name="email">The new email.</param>
        /// <returns><see cref="true"/> if the change was successful</returns>
        [HttpPost]
        public bool ChangeEmail(Guid personId, string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a password from a Person
        /// </summary>
        /// <param name="personId">The ID of the person whose email is to be changed.</param>
        /// <param name="oldPassword">The current password</param>
        /// <param name="newPassword">The chosen password</param>
        /// <param name="retypedNewPassword">The personId of the registered person or <see cref="Guid.Empty"/> if login failed</param>
        /// <returns><see cref="true"/> if the change was successful</returns>
        [HttpPost]
        public bool ChangePassword(Guid personId, string oldPassword, string newPassword, string retypedNewPassword)
        {
            throw new NotImplementedException();
        }


    }
}
