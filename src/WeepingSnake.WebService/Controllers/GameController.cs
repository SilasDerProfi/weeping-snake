using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeepingSnake.Game.Person;
using WeepingSnake.Game.Player;
using WeepingSnake.Game.Structs;

namespace WeepingSnake.WebService.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }
        
        /// <summary>
        /// Register for a new game as guest. The result is a unique playerId (only valid for exactly this game).
        /// </summary>
        /// <returns>A unique player id for exactly one game.</returns>
        [HttpPut]
        public async Task<ActionResult<Guid>> JoinGame()
        {
            var joinedPlayer = GameBackend.Controller.JoinGame();
            return Ok(joinedPlayer.PlayerId);
        }

        /// <summary>
        /// Register for a new game as registered person. The result is a unique playerId (only valid for exactly this game).
        /// </summary>
        /// <param name="personId">The personId of the one who wants to participate in a game.</param>
        /// <returns>A unique player id for exactly one game.</returns>
        [HttpPut("{personId:guid}")]
        public async Task<ActionResult<Guid>> JoinGame(Guid personId)
        {
            var person = Person.GetById(personId);

            if (person != null)
            {
                var joinedPlayer = GameBackend.Controller.JoinGame(person);
                return Ok(joinedPlayer.PlayerId);
            }

            return BadRequest("Invalid personId");
        }

        /// <summary>
        /// Requests the game state for a game in which a specific player is participating.
        /// </summary>
        /// <param name="playerId">The id of the player for which the game state is requested.</param>
        /// <returns>The state of the game, if the specified player exists and is involved in a game.</returns>
        [HttpGet("{playerId:guid}")]
        public async Task<ActionResult<object>> GetGameState(Guid playerId)
        {
            var player = GameBackend.Controller.FindPlayer(playerId);

            if (player == null)
                return BadRequest("Invalid playerId");

            var playerData = new 
            { 
                Points = player.Points, 
                IsAlive = player.IsAlive 
            };

            var allPaths = player.AssignedGame.BoardPaths;

            var currentRoundNumber = allPaths.Count;
            var playerLinesStartingRoundIndex = Math.Max(0, currentRoundNumber - 6);
            var roundsWithPlayerLines = currentRoundNumber - playerLinesStartingRoundIndex;

            var validGameDistances = allPaths.GetRange(playerLinesStartingRoundIndex, roundsWithPlayerLines).SelectMany(paths => paths).ToList();

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
                BoardHeight = player.AssignedGame.GameBoard.Height,
                BoardWidth = player.AssignedGame.GameBoard.Width,
                Board = boardData
            };

            return Ok(gameState);
        }

        /// <summary>
        /// Adds an action to be performed for a player.
        /// </summary>
        /// <param name="playerId">The id of the player that should perform this action.</param>
        /// <param name="action">The action to be performed.</param>
        [HttpPost("{playerId:guid}")]
        public async Task<IActionResult> SendAction(Guid playerId, PlayerAction.Action action)
        {
            var player = GameBackend.Controller.FindPlayer(playerId);
            
            if(player == null)
            {
                return BadRequest("Invalid playerId");
            }

            GameBackend.Controller.DoAction(player, action);

            return Ok();
        }
    }
}
