using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PaaspopService.Common.DictionaryAsArrayResolver;

namespace PaaspopService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        protected IMediator Mediator;
        protected JsonSerializerSettings JsonDictionaryAsArrayResolver;

        public BaseController()
        {
            JsonDictionaryAsArrayResolver = new JsonSerializerSettings
            {
                ContractResolver = new DictionaryAsArrayResolver(),
            };
        }

        protected IMediator GetMediator()
        {
            return Mediator ?? (Mediator = HttpContext.RequestServices.GetService<IMediator>());
        }
    }
}