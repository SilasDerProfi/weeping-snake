using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WeepingSnake.Game.Person;

namespace WeepingSnake.WebService.Controllers
{
    [ApiController]
    [Route("api/highscores")]
    public class HighscoreController : ControllerBase
    {
        private readonly ILogger<HighscoreController> _logger;
        private const int EntriesPerPage = 20;

        public HighscoreController(ILogger<HighscoreController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Returns a Page from the Highscore-List
        /// </summary>
        /// <returns>List of <see cref="EntriesPerPage"/> Highscore entries</returns>
        [HttpGet("{pageNumber:int}")]
        public async Task<ActionResult<List<object>>> GetHighscores(int pageNumber)
        {
            var entriesToSkip = pageNumber * EntriesPerPage;

            var highscores = HighscoreEntry.GetHighscoreEntries()
                .OrderByDescending(h => h.MaximumPointsInGame)
                .Skip(entriesToSkip)
                .Take(EntriesPerPage)
                .ToList();

            if (highscores.Count == 0)
            {
                return NotFound("The searched Page is out of range.");
            }

            var mappedHighscores = new List<object>();
            for (var highscoreIndex = 1; highscoreIndex <= highscores.Count; highscoreIndex++)
            {
                var highscoreEntry = highscores[highscoreIndex];

                var mappedHighscore = new
                {
                    Placement = $"#{entriesToSkip + highscoreIndex}",
                    UserName = highscoreEntry.Username,
                    Highscore = highscoreEntry.MaximumPointsInGame,
                    NumerOfPlayedGames = highscoreEntry.PlayedGames,
                    HighScoreSum = highscoreEntry.TotalPoints
                };

                mappedHighscores.Add(mappedHighscore);
            }

            return Ok(mappedHighscores);
        }
    }
}
