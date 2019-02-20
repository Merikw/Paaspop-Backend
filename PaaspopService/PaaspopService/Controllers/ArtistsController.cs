using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaaspopService.Application.Artists.Models;
using PaaspopService.Application.Artists.Queries;

namespace PaaspopService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : BaseController
    {
        // GET: api/Artist/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<ArtistViewModel>> Get(int id)
        {
            return Ok(await GetMediator().Send(new GetArtistQuery { Id = id }));
        }
    }
}
