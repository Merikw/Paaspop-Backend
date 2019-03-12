using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaaspopService.Application.Performances.Queries;
using PaaspopService.Application.Places.Queries.GetBestPlacesQuery;

namespace PaaspopService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : BaseController
    {
        [HttpGet("best")]
        public async Task<ActionResult<PerformanceViewModel>> GetBest()
        {
            var result = await GetMediator().Send(new GetBestPlacesQuery());
            var arrayDictResult = JsonConvert.SerializeObject(result, JsonDictionaryAsArrayResolver);
            return Ok(arrayDictResult);
        }
    }
}
