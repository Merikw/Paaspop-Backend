using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaaspopService.Application.Performances.Queries.GetPerformances;
using PaaspopService.Application.Performances.Queries.GetPerformancesById;
using PaaspopService.Domain.Entities;

namespace PaaspopService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : BaseController
    {
        /// <summary>
        /// API call to get all the performances in combination with the suggested performances from an user.
        /// </summary>
        /// <param name="userId">The user that wants to receive the performances with their personalized performance suggestions.</param>
        /// <returns>A dictionary which includes the stage as the key and a list with performance objects as the value and a list of the 10 suggested
        /// values for that specific user.</returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<PerformanceViewModel>> Get(string userId)
        {
            var result = await GetMediator().Send(new GetPerformancesQuery { UserId =  userId });
            var arrayDictResult = JsonConvert.SerializeObject(result, JsonDictionaryAsArrayResolver);
            return Ok(arrayDictResult);
        }

        /// <summary>
        /// API call to get a single performance object by the Id of that performance.
        /// </summary>
        /// <param name="performanceId">The Id of the performance that the user wants to receive.</param>
        /// <returns>The performance object corresponding to the Id.</returns>
        [HttpGet("byid/{performanceId}")]
        public async Task<ActionResult<Performance>> GetById(string performanceId)
        {
            var result = await GetMediator().Send(new GetPerformanceByIdQuery { Id = performanceId });
            return Ok(result);
        }
    }
}