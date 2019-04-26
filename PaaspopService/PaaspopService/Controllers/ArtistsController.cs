using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaaspopService.Application.Artists.Queries;

namespace PaaspopService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : BaseController
    {
        /// <summary>
        /// Test method to get an artist. Purely for testing and review purposes.
        /// </summary>
        /// <param name="id">Id of the artists that you want to get</param>
        /// <returns>The artist object found with that Id</returns>
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<ArtistViewModel>> Get(string id)
        {
            return Ok(await GetMediator().Send(new GetArtistQuery {Id = id}));
        }
    }
}