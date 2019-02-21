using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaaspopService.Application.Artists.Queries;

namespace PaaspopService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : BaseController
    {
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<ArtistViewModel>> Get(string id)
        {
            return Ok(await GetMediator().Send(new GetArtistQuery {Id = id}));
        }
    }
}