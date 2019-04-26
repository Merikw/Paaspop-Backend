using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaaspopService.Application.Performances.Queries.GetFavoritePerformancesFromUser;
using PaaspopService.Application.Users.Commands.CreateUser;
using PaaspopService.Application.Users.Commands.RemoveUser;
using PaaspopService.Application.Users.Commands.UpdateUser;
using PaaspopService.Domain.Entities;

namespace PaaspopService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        /// <summary>
        /// Gets the favorite performances from an user.
        /// </summary>
        /// <param name="userId">The id of the user that the API user wants to know their favorites of.</param>
        /// <returns>A list of performance objects which are the favorites of that user.</returns>
        [HttpGet("favoritePerformances/{userId}")]
        public async Task<ActionResult<List<Performance>>> GetFavoritePerformances(string userId)
        {
            var result = await GetMediator().Send(new GetFavoritePerformancesFromUserQuery {UserId = userId});

            return Ok(result);
        }

        /// <summary>
        /// Post a new user in the database.
        /// </summary>
        /// <param name="command">The user that needs to be inserted into the database.</param>
        /// <returns>The user that is inserted into the database.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await GetMediator().Send(command);

            return Ok(result);
        }

        /// <summary>
        /// Update an existing user into the database.
        /// </summary>
        /// <param name="command">The user that needs to be updated in the database.</param>
        /// <returns>The user that is updated ins the database.</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {

            var result = await GetMediator().Send(command);

            return Ok(result);
        }

        /// <summary>
        /// Remove an existing user from the database.
        /// </summary>
        /// <param name="userId">The id of the user that needs to be removed from the database.</param>
        /// <returns>Void and status code 200 when succeeded.</returns>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Remove(string userId)
        {

            var result = await GetMediator().Send(new RemoveUserCommand() { UserId = userId });

            return Ok(result);
        }
    }
}