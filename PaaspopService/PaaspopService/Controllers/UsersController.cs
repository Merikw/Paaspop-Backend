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
        [HttpGet("favoritePerformances/{userId}")]
        public async Task<ActionResult<List<Performance>>> GetBest(string userId)
        {
            var result = await GetMediator().Send(new GetFavoritePerformancesFromUserQuery {UserId = userId});

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await GetMediator().Send(command);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {

            var result = await GetMediator().Send(command);

            return Ok(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Remove(string userId)
        {

            var result = await GetMediator().Send(new RemoveUserCommand() { UserId = userId });

            return Ok(result);
        }
    }
}