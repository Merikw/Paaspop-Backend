using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaaspopService.Application.Performances.Queries;
using PaaspopService.Application.Places.Queries.GetBestPlacesQuery;
using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : BaseController
    {
        [HttpGet("best/{lat}/{lon}")]
        public async Task<ActionResult<PerformanceViewModel>> GetBest(double lat, double lon)
        {
            var result = await GetMediator().Send(new GetBestPlacesQuery
            {
                UserLocationCoordinate = new LocationCoordinate(lat, lon)
            });
            var arrayDictResult = JsonConvert.SerializeObject(result, JsonDictionaryAsArrayResolver);
            return Ok(arrayDictResult);
        }
    }
}
