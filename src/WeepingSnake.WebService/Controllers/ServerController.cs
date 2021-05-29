using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WeepingSnake.WebService.Controllers
{
    [Route("api/time")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly ILogger<ServerController> _logger;

        public ServerController(ILogger<ServerController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Request the current time of the server.
        /// </summary>
        /// <returns>The time of the server when the request was processed (DateTimeOffSet).</returns>
        [HttpGet]
        public async Task<ActionResult<DateTimeOffset>> GetServerTime()
        {
            var currentTime = DateTimeOffset.Now;

            return Ok(currentTime);
        }
    }
}
