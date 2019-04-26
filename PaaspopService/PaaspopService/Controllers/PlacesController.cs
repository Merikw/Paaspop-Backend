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
        /// <summary>
        /// Gets all the places in the order that are the best for the user's current location (latitude and longitude).
        /// </summary>
        /// <param name="lat">The latitude of the current location of the user.</param>
        /// <param name="lon">The longitude of the current location of the user.</param>
        /// <returns>A dictionary which includes the type of place as the key and a list of those places as the value. Furthermore,
        /// it will return an integer called maxPercentage to get the max percentage of the busiest place.</returns>
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

        /// <summary>
        /// API call to generate and get a meeting point which is the closest to the current location of the user who wants to generate
        /// a meeting point.
        /// </summary>
        /// <param name="lat">The latitude of the current location of the user.</param>
        /// <param name="lon">The longitude of the current location of the user.</param>
        /// <returns>A dictionary which includes the stage as the key and a list with performance objects as the value and a list of the 10 suggested
        /// values for that specific user.</returns>
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
