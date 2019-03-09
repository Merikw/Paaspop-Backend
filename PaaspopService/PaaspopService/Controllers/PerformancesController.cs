using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaaspopService.Application.Performances.Queries;
using PaaspopService.Common.DictionaryAsArrayResolver;

namespace PaaspopService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : BaseController
    {
        public async Task<ActionResult<PerformanceViewModel>> Get()
        {
            var result = await GetMediator().Send(new GetPerformancesQuery());
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ContractResolver = new DictionaryAsArrayResolver();

            var arrayDictResult = JsonConvert.SerializeObject(result, settings);
            return Ok(arrayDictResult);
        }
    }
}