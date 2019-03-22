using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaaspopService.Application.Performances.Queries;
using PaaspopService.Application.Performances.Queries.GetPerformances;

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
    }
}