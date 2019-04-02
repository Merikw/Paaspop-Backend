using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaaspopService.Application.Places.Queries.GetBestPlacesQuery;
using PaaspopService.Application.Places.Queries.GetMeetingPointQuery;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.ValueObjects;

namespace PaaspopService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : BaseController
    {
        [HttpGet("best/{lat}/{lon}")]
        public async Task<ActionResult<BestPlacesViewModel>> GetBest(double lat, double lon)
        {
            var result = await GetMediator().Send(new GetBestPlacesQuery
            {
                UserLocationCoordinate = new LocationCoordinate(lat, lon)
            });
            var arrayDictResult = JsonConvert.SerializeObject(result, JsonDictionaryAsArrayResolver);
            return Ok(arrayDictResult);
        }

        [HttpGet("generateMeetingPoint/{lat}/{lon}")]
        public async Task<ActionResult<Place>> GenerateMeetingPoint(double lat, double lon)
        {
            var result = await GetMediator().Send(new GetMeetingPointQuery
            {
                LocationOfUser = new LocationCoordinate(lat, lon)
            });
            return Ok(result);
        }
    }
}
