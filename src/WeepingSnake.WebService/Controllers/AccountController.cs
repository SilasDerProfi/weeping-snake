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
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Registers a new Person
        /// </summary>
        /// <param name="email">Email as reference for the person (have to be unique)</param>
        /// <param name="username">wished username (does not have to be unique)</param>
        /// <param name="password">chosen password</param>
        /// <param name="retypePassword">retyped password to avoid typos</param>
        /// <returns>The personId of the registered person</returns>
        [HttpPut]
        public async Task<ActionResult<Guid>> Register(string email, string username, string password, string retypePassword)
        {
            Person.Register(email, username, password, retypePassword);

            var person = Person.Login(email, password);

            if (person?.PersonId == null)
            {
                return BadRequest("Email address is already known, or the retyped password is wrong");
            }

            return Ok(person.PersonId);
        }

        /// <summary>
        /// Login a already registered Person
        /// </summary>
        /// <param name="email">The email linked to the person</param>
        /// <param name="password">The chosen password</param>
        /// <returns>The personId of the registered person</returns>
        [HttpGet]
        public async Task<ActionResult<Guid>> Login(string email, string password)
        {
            var person = Person.Login(email, password);

            if (person == null)
            {
                return BadRequest("Invalid combination of Email address and password");
            }

            return Ok(person.PersonId);
        }

        /// <summary>
        /// Updates an email from a Person
        /// </summary>
        /// <param name="personId">The ID of the person whose email is to be changed.</param>
        /// <param name="email">The new email.</param>
        [HttpPost("{personId:guid}/email")]
        public async Task<IActionResult> ChangeEmail(Guid personId, string email)
        {
            var person = Person.GetById(personId);

            if (person == null)
            {
                return NotFound("Can't find the Person with the given ID");
            }

            if (person.ChangeEmail(email))
            {
                return Ok();
            }

            return BadRequest("The given Email address is invalid or already in use");
        }

        /// <summary>
        /// Updates a password from a Person
        /// </summary>
        /// <param name="personId">The ID of the person whose email is to be changed.</param>
        /// <param name="oldPassword">The current password</param>
        /// <param name="newPassword">The chosen password</param>
        /// <param name="retypedNewPassword">The personId of the registered person or <see cref="Guid.Empty"/> if login failed</param>
        /// <returns><see cref="true"/> if the change was successful</returns>
        [HttpPost("{personId:guid}/password")]
        public async Task<IActionResult> ChangePassword(Guid personId, string oldPassword, string newPassword, string retypedNewPassword)
        {
            var person = Person.GetById(personId);

            if (person == null)
            {
                return NotFound("Can't find the Person with the given ID");
            }

            if (Person.Login(person.MailAddress.Address, oldPassword) == null)
            {
                return BadRequest("Old password is invalid");
            }

            if (person.ChangePassword(newPassword, retypedNewPassword))
            {
                return Ok();
            }

            return BadRequest("The retyped password is wrong");
        }
    }
}
