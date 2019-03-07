using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaaspopService.Application.Performances.Queries;

namespace PaaspopService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : BaseController
    {
        public async Task<ActionResult<PerformanceViewModel>> Get()
        {
            return Ok(await GetMediator().Send(new GetPerformancesQuery()));
        }
    }
}