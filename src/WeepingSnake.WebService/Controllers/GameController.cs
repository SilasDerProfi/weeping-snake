using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
namespace WeepingSnake.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public Guid JoinGame() => throw new NotImplementedException();

        /// <summary>
        /// Register for a new game as registered person. The result is a unique playerId (only valid for exactly this game).
        /// </summary>
        /// <param name="personId">The personId of the one who wants to participate in a game.</param>
        /// <returns>A unique player id for exactly one game.</returns>
        [HttpPut("{id}")]
        public Guid JoinGame(Guid personId) => throw new NotImplementedException();

        /// <summary>
        /// Requests the game state for a game in which a specific player is participating.
        /// </summary>
        /// <param name="playerId">The id of the player for which the game state is requested.</param>
        /// <returns>The state of the game, if the specified player exists and is involved in a game.</returns>
        [HttpGet("{id}")]
        public object GetGameState(Guid playerId) => throw new NotImplementedException();

        /// <summary>
        /// Adds an action to be performed for a player.
        /// </summary>
        /// <param name="playerId">The id of the player that should perform this action.</param>
        /// <param name="action">The action to be performed.</param>
        /// <returns>The entire list of actions still to be performed.</returns>
        [HttpPost]
        public object SendAction(Guid playerId, object action) => throw new NotImplementedException();

        /// <summary>
        /// Discards all actions sent by a player that are not performed yet.
        /// </summary>
        /// <param name="playerId">The id of the player which actions should be discarded.</param>
        /// <returns>The entire list of actions still to be performed.</returns>
        [HttpPost]
        public object ClearActions(Guid playerId) => throw new NotImplementedException();

        /// <summary>
        /// Request the current time of the server.
        /// </summary>
        /// <returns>The time of the server when the request was processed (DateTimeOffSet).</returns>
        [HttpGet]
        public DateTimeOffset GetServerTime() => throw new NotImplementedException();
    }
}
