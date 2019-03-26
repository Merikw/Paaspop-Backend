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
        [HttpGet("{userId}")]
        public async Task<ActionResult<PerformanceViewModel>> Get(string userId)
        {
            var result = await GetMediator().Send(new GetPerformancesQuery { UserId =  userId });
            var arrayDictResult = JsonConvert.SerializeObject(result, JsonDictionaryAsArrayResolver);
            return Ok(arrayDictResult);
        }

        [HttpGet("byid/{performanceId}")]
        public async Task<ActionResult<Performance>> GetById(string performanceId)
        {
            var result = await GetMediator().Send(new GetPerformanceByIdQuery { Id = performanceId });
            return Ok(result);
        }
    }
}