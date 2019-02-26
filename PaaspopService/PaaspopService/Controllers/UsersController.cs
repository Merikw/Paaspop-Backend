using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaaspopService.Application.Artists.Queries;
using PaaspopService.Application.Users.Commands.CreateUser;

namespace PaaspopService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateUserCommand command)
        {
            await GetMediator().Send(command);

            return Ok();
        }
    }
}
