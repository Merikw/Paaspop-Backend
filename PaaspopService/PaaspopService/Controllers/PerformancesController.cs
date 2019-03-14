using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaaspopService.Application.Performances.Queries;

namespace PaaspopService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<PerformanceViewModel>> Get()
        {
            var result = await GetMediator().Send(new GetPerformancesQuery());
            var arrayDictResult = JsonConvert.SerializeObject(result, JsonDictionaryAsArrayResolver);
            return Ok(arrayDictResult);
        }
    }
}