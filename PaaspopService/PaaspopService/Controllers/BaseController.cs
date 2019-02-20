using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
namespace PaaspopService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        private IMediator _mediator;

        protected IMediator GetMediator()
        {
            if(_mediator == null)
            {
                _mediator = HttpContext.RequestServices.GetService<IMediator>();
            };

            return _mediator;
        }
    }
}